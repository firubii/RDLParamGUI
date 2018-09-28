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
    public partial class Form1 : Form
    {
        Dictionary<string, byte[]> paramFiles = new Dictionary<string, byte[]>();
        Dictionary<string, uint[]> paramData = new Dictionary<string, uint[]>();
        Dictionary<string, uint[]> originalData = new Dictionary<string, uint[]>();
        IniData labelData = new IniData();
        string filepath;

        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateINI()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\labels.ini"))
            {
                labelData = new FileIniDataParser().ReadFile(Directory.GetCurrentDirectory() + "\\labels.ini");
            }
            valueList.Items.Clear();
            int index = fileList.SelectedIndex;
            UpdateFileList();
            fileList.SelectedIndex = index;
        }

        public uint[] ReadParams(byte[] file)
        {
            List<uint> uintList = new List<uint>();
            uint fileSize = ReverseBytes(BitConverter.ToUInt32(file, 0x8));
            for (int i = 0x10; i < fileSize; i += 0x4)
            {
                try
                {
                    uintList.Add(ReverseBytes(BitConverter.ToUInt32(file, i)));
                } catch { }
            }
            return uintList.ToArray();
        }

        public void UpdateFileList()
        {
            fileList.Items.Clear();
            foreach (KeyValuePair<string, byte[]> file in paramFiles)
            {
                fileList.Items.Add(file.Key);
            }
        }

        public void Save()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            List<byte[]> files = new List<byte[]>();
            List<string> fileNames = new List<string>();
            List<byte> archive = new List<byte>()
            {
                0x58, 0x42, 0x49, 0x4E, 0x12, 0x34, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0xA4
            };
            for (int i = 0; i < fileList.Items.Count; i++)
            {
                List<byte> file = new List<byte>()
                {
                    0x58, 0x42, 0x49, 0x4E, 0x12, 0x34, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0xA4
                };
                string name = fileList.Items[i].ToString();
                fileNames.Add(name);
                for (int v = 0; v < paramData[name].Length; v++)
                {
                    file.AddRange(BitConverter.GetBytes(ReverseBytes(paramData[name][v])));
                }
                while (file.Count.ToString("X").Last() != '0' && file.Count.ToString("X").Last() != '4' && file.Count.ToString("X").Last() != '8' && file.Count.ToString("X").Last() != 'C')
                {
                    file.Add(0x00);
                }
                file.RemoveRange(0x8, 0x4);
                file.InsertRange(0x8, BitConverter.GetBytes(ReverseBytes((uint)file.Count + 0x4)));
                files.Add(file.ToArray());
            }
            archive.AddRange(BitConverter.GetBytes(ReverseBytes((uint)paramData.Count)));
            List<uint> fileOffsets = new List<uint>();
            List<uint> nameOffsets = new List<uint>();
            for (int i = 0; i < files.Count; i++)
            {
                archive.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            }
            for (int i = 0; i < files.Count; i++)
            {
                fileOffsets.Add((uint)archive.Count);
                archive.AddRange(files[i]);
            }
            for (int i = 0; i < files.Count; i++)
            {
                nameOffsets.Add((uint)archive.Count);
                archive.AddRange(BitConverter.GetBytes(ReverseBytes((uint)fileNames[i].Length)));
                archive.AddRange(Encoding.UTF8.GetBytes(fileNames[i]));
                while (archive.Count.ToString("X").Last() != '0' && archive.Count.ToString("X").Last() != '4' && archive.Count.ToString("X").Last() != '8' && archive.Count.ToString("X").Last() != 'C')
                {
                    archive.Add(0x00);
                }
            }
            for (int i = 0; i < files.Count; i++)
            {
                archive.RemoveRange(0x14 + (i * 0x8), 0x4);
                archive.InsertRange(0x14 + (i * 0x8), BitConverter.GetBytes(ReverseBytes(nameOffsets[i])));
                archive.RemoveRange(0x18 + (i * 0x8), 0x4);
                archive.InsertRange(0x18 + (i * 0x8), BitConverter.GetBytes(ReverseBytes(fileOffsets[i])));
            }
            archive.RemoveRange(0x8, 0x4);
            archive.InsertRange(0x8, BitConverter.GetBytes(ReverseBytes((uint)archive.Count + 0x4)));
            File.WriteAllBytes(filepath, archive.ToArray());
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XBIN BIN Archives|*.bin";
            if (open.ShowDialog() == DialogResult.OK)
            {
                filepath = open.FileName;
                byte[] archive = File.ReadAllBytes(filepath);
                if (Encoding.UTF8.GetString(archive, 0, 4) == "XBIN")
                {
                    uint fileCount = ReverseBytes(BitConverter.ToUInt32(archive, 0x10));
                    for (int i = 0; i < fileCount; i++)
                    {
                        uint nameOffset = ReverseBytes(BitConverter.ToUInt32(archive, 0x14 + (i * 0x8)));
                        uint fileOffset = ReverseBytes(BitConverter.ToUInt32(archive, 0x18 + (i * 0x8)));
                        uint nameLength = ReverseBytes(BitConverter.ToUInt32(archive, (int)nameOffset));
                        uint fileLength = ReverseBytes(BitConverter.ToUInt32(archive, (int)fileOffset + 0x8));
                        string fileName = Encoding.UTF8.GetString(archive, (int)nameOffset + 0x4, (int)nameLength);
                        byte[] file = archive.Skip((int)fileOffset).Take((int)fileLength).ToArray();
                        paramFiles.Add(fileName, file);
                        paramData.Add(fileName, ReadParams(file));
                        originalData = paramData;
                    }
                    UpdateFileList();
                }
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
                try
                {
                    valueList.Items.Add(labelData.Sections[filename][$"{i}"]);
                }
                catch
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

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateINI();
        }
        
        public uint ReverseBytes(uint val)
        {
            return (val & 0x000000FF) << 24 |
                    (val & 0x0000FF00) << 8 |
                    (val & 0x00FF0000) >> 8 |
                    ((uint)(val & 0xFF000000)) >> 24;
        }
        public int ReverseBytes(int val)
        {
            return (val & 0x000000FF) << 24 |
                    (val & 0x0000FF00) << 8 |
                    (val & 0x00FF0000) >> 8 |
                    ((int)(val & 0xFF000000)) >> 24;
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
            save.Filter = "XBIN BIN Archives|*.bin";
            save.DefaultExt = ".bin";
            if (save.ShowDialog() == DialogResult.OK)
            {
                filepath = save.FileName;
                Save();
            }
        }
    }
}
