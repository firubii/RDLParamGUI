using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IniParser;
using IniParser.Exceptions;
using IniParser.Model;
using IniParser.Parser;

namespace RDLParamGUI
{
    public enum Endianness
    {
        Little,
        Big
    }

    public partial class Form1 : Form
    {
        Endianness endianness;
        uint unkXbin;
        Dictionary<string, uint[]> paramData;
        Dictionary<string, uint[]> originalData;
        IniData labelData = new IniData();
        string filepath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateINI();
        }

        public void UpdateINI()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\labels.ini"))
            {
                labelData = new FileIniDataParser().ReadFile(Directory.GetCurrentDirectory() + "\\labels.ini");
            }
            valueList.Items.Clear();
            int index = fileList.SelectedIndex;
            if (paramData != null)
                UpdateFileList();
            fileList.SelectedIndex = index;
        }

        public uint[] ReadParams(byte[] file)
        {
            List<uint> uintList = new List<uint>();
            BinaryReader reader = new BinaryReader(new MemoryStream(file));
            if (endianness == Endianness.Big)
                reader = new BigEndianBinaryReader(new MemoryStream(file));

            reader.BaseStream.Seek(0x10, SeekOrigin.Begin);
            while (reader.BaseStream.Position < reader.BaseStream.Length - 3)
            {
                uintList.Add(reader.ReadUInt32());
            }

            return uintList.ToArray();
        }

        public void UpdateFileList()
        {
            fileList.Items.Clear();
            fileList.BeginUpdate();
            foreach (KeyValuePair<string, uint[]> file in paramData)
            {
                fileList.Items.Add(file.Key);
            }
            fileList.EndUpdate();
        }

        public void Save()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            List<byte[]> files = new List<byte[]>();
            List<string> fileNames = new List<string>();
            BinaryWriter writer;

            foreach (KeyValuePair<string, uint[]> pair in paramData)
            {
                MemoryStream paramFile = new MemoryStream();
                if (endianness == Endianness.Big)
                    writer = new BigEndianBinaryWriter(paramFile);
                else
                    writer = new BinaryWriter(paramFile);

                writer.Write("XBIN".ToCharArray());
                writer.Write((short)0x1234);
                writer.Write(new byte[] { 2, 0 });
                writer.Write(0);
                writer.Write(unkXbin);

                for (int i = 0; i < pair.Value.Length; i++)
                {
                    writer.Write(pair.Value[i]);
                }

                writer.BaseStream.Seek(0x8, SeekOrigin.Begin);
                writer.Write((uint)writer.BaseStream.Length);
                paramFile.SetLength(writer.BaseStream.Length);

                fileNames.Add(pair.Key);
                files.Add(paramFile.GetBuffer().Take((int)writer.BaseStream.Length).ToArray());

                writer.Close();
                writer.Dispose();
            }

            if (endianness == Endianness.Big)
                writer = new BigEndianBinaryWriter(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write));
            else
                writer = new BinaryWriter(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write));

            writer.Write("XBIN".ToCharArray());
            writer.Write((short)0x1234);
            writer.Write(new byte[] { 2, 0 });
            writer.Write(0);
            writer.Write(unkXbin);

            writer.Write((int)paramData.Count);

            List<uint> fileOffsets = new List<uint>();
            List<uint> nameOffsets = new List<uint>();
            for (int i = 0; i < files.Count; i++)
            {
                writer.Write((long)0);
            }
            for (int i = 0; i < files.Count; i++)
            {
                fileOffsets.Add((uint)writer.BaseStream.Position);
                writer.Write(files[i]);
                //Console.WriteLine($"Wrote {files[i].Length} bytes of file {fileNames[i]}");
            }
            for (int i = 0; i < fileNames.Count; i++)
            {
                Console.WriteLine($"Writing string {fileNames[i]}");
                long o = writer.BaseStream.Position;
                nameOffsets.Add((uint)writer.BaseStream.Position);
                writer.Write(fileNames[i].Length);
                writer.Write(Encoding.UTF8.GetBytes(fileNames[i]));
                writer.Write(0);

                while (!writer.BaseStream.Position.ToString("X").EndsWith("0")
                    && !writer.BaseStream.Position.ToString("X").EndsWith("4")
                    && !writer.BaseStream.Position.ToString("X").EndsWith("8")
                    && !writer.BaseStream.Position.ToString("X").EndsWith("C"))
                {
                    writer.Write((byte)0);
                }
                //Console.WriteLine($"Wrote {writer.BaseStream.Position - o} bytes");
            }
            writer.BaseStream.Seek(0x14, SeekOrigin.Begin);
            for (int i = 0; i < files.Count; i++)
            {
                writer.Write(nameOffsets[i]);
                writer.Write(fileOffsets[i]);
            }

            writer.BaseStream.Seek(0x8, SeekOrigin.Begin);
            writer.Write((uint)writer.BaseStream.Length);

            writer.Close();
            writer.Dispose();

            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XBIN Binary Archives|*.bin";
            if (open.ShowDialog() == DialogResult.OK)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;

                paramData = new Dictionary<string, uint[]>();

                filepath = open.FileName;
                BinaryReader reader = new BinaryReader(new FileStream(filepath, FileMode.Open, FileAccess.Read));

                if (Encoding.UTF8.GetString(reader.ReadBytes(4)) != "XBIN")
                {
                    MessageBox.Show("Invalid XBIN header!", this.Text, MessageBoxButtons.OK);
                    return;
                }

                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                endianness = Endianness.Little;
                if (reader.ReadBytes(2).SequenceEqual(new byte[] { 0x12, 0x34 }))
                {
                    reader = new BigEndianBinaryReader(new FileStream(filepath, FileMode.Open, FileAccess.Read));
                    endianness = Endianness.Big;
                }

                reader.BaseStream.Seek(0xC, SeekOrigin.Begin);
                unkXbin = reader.ReadUInt32();

                uint fileCount = reader.ReadUInt32();
                for (int i = 0; i < fileCount; i++)
                {
                    long pos = reader.BaseStream.Position;

                    reader.BaseStream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);
                    string name = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));

                    reader.BaseStream.Seek(pos + 0x4, SeekOrigin.Begin);
                    reader.BaseStream.Seek(reader.ReadUInt32() + 0x8, SeekOrigin.Begin);
                    int len = reader.ReadInt32();
                    reader.BaseStream.Seek(-0xC, SeekOrigin.Current);
                    byte[] file = reader.ReadBytes(len);

                    paramData.Add(name, ReadParams(file));

                    reader.BaseStream.Seek(pos + 0x8, SeekOrigin.Begin);
                }
                originalData = paramData;

                reader.Close();
                reader.Dispose();

                UpdateFileList();

                this.Cursor = Cursors.Default;
                this.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueList.Items.Clear();
            hexData.Text = "";
            intData.Text = "";
            floatData.Text = "";
            hexDataOrig.Text = "";
            intDataOrig.Text = "";
            floatDataOrig.Text = "";
            string filename = fileList.SelectedItem.ToString();
            uint[] values = paramData[filename];
            for (int i = 0; i < values.Length; i++)
            {
                if (labelData.Sections.ContainsSection(filename))
                {
                    if (labelData.Sections[filename].ContainsKey(i.ToString()))
                    {
                        valueList.Items.Add(labelData.Sections[filename][i.ToString()]);
                    }
                }
                else
                {
                    valueList.Items.Add("Entry " + i);
                }
            }
        }

        private void valueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = valueList.SelectedIndex;
            uint[] origData = originalData[fileList.SelectedItem.ToString()];
            uint[] data = paramData[fileList.SelectedItem.ToString()];
            byte[] origFloatBytes = BitConverter.GetBytes(origData[index]);
            byte[] floatBytes = BitConverter.GetBytes(data[index]);
            hexData.Text = data[index].ToString("X8");
            intData.Text = data[index].ToString();
            floatData.Text = BitConverter.ToSingle(floatBytes, 0).ToString();
            hexDataOrig.Text = origData[index].ToString("X8");
            intDataOrig.Text = origData[index].ToString();
            floatDataOrig.Text = BitConverter.ToSingle(origFloatBytes, 0).ToString();
        }

        private void updateLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateINI();
        }

        private void hexData_TextChanged(object sender, EventArgs e)
        {
            if (hexData.Text != "" && intData.Text != "" && floatData.Text != "" && hexData.Focused)
            {
                int index = valueList.SelectedIndex;
                uint[] data = paramData[fileList.SelectedItem.ToString()];
                byte[] floatBytes = BitConverter.GetBytes(uint.Parse(hexData.Text, System.Globalization.NumberStyles.HexNumber));
                intData.Text = uint.Parse(hexData.Text, System.Globalization.NumberStyles.HexNumber).ToString();
                floatData.Text = BitConverter.ToSingle(floatBytes, 0).ToString();
                data[index] = uint.Parse(hexData.Text, System.Globalization.NumberStyles.HexNumber);
                paramData[fileList.SelectedItem.ToString()] = data;
            }
        }

        private void intData_TextChanged(object sender, EventArgs e)
        {
            if (hexData.Text != "" && intData.Text != "" && floatData.Text != "" && intData.Focused)
            {
                int index = valueList.SelectedIndex;
                uint[] data = paramData[fileList.SelectedItem.ToString()];
                byte[] floatBytes = BitConverter.GetBytes(uint.Parse(intData.Text));
                hexData.Text = uint.Parse(intData.Text).ToString("X8");
                floatData.Text = BitConverter.ToSingle(floatBytes, 0).ToString();
                data[index] = uint.Parse(intData.Text);
                paramData[fileList.SelectedItem.ToString()] = data;
            }
        }

        private void floatData_TextChanged(object sender, EventArgs e)
        {
            if (hexData.Text != "" && intData.Text != "" && floatData.Text != "" && floatData.Focused)
            {
                try
                {
                    int index = valueList.SelectedIndex;
                    uint[] data = paramData[fileList.SelectedItem.ToString()];
                    byte[] floatBytes = BitConverter.GetBytes(float.Parse(floatData.Text));
                    uint parsedFloat = BitConverter.ToUInt32(floatBytes, 0);
                    hexData.Text = parsedFloat.ToString("X8");
                    intData.Text = parsedFloat.ToString();
                    data[index] = uint.Parse(hexData.Text, System.Globalization.NumberStyles.HexNumber);
                    paramData[fileList.SelectedItem.ToString()] = data;
                } catch { }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.AddExtension = true;
            save.Filter = "XBIN Binary Archives|*.bin";
            save.DefaultExt = ".bin";
            if (save.ShowDialog() == DialogResult.OK)
            {
                filepath = save.FileName;
                Save();
            }
        }
    }
}
