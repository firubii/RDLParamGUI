namespace RDLParamGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.fileList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.valueList = new System.Windows.Forms.ListBox();
            this.hexData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.intData = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.floatData = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.floatDataOrig = new System.Windows.Forms.TextBox();
            this.hexDataOrig = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.intDataOrig = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.updateLabelsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(616, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // updateLabelsToolStripMenuItem
            // 
            this.updateLabelsToolStripMenuItem.Name = "updateLabelsToolStripMenuItem";
            this.updateLabelsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.updateLabelsToolStripMenuItem.Text = "Update Labels";
            this.updateLabelsToolStripMenuItem.Click += new System.EventHandler(this.updateLabelsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Param Files";
            // 
            // fileList
            // 
            this.fileList.FormattingEnabled = true;
            this.fileList.Location = new System.Drawing.Point(16, 45);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(200, 355);
            this.fileList.TabIndex = 2;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Values";
            // 
            // valueList
            // 
            this.valueList.FormattingEnabled = true;
            this.valueList.Location = new System.Drawing.Point(222, 45);
            this.valueList.Name = "valueList";
            this.valueList.Size = new System.Drawing.Size(228, 355);
            this.valueList.TabIndex = 5;
            this.valueList.SelectedIndexChanged += new System.EventHandler(this.valueList_SelectedIndexChanged);
            // 
            // hexData
            // 
            this.hexData.Location = new System.Drawing.Point(9, 33);
            this.hexData.Name = "hexData";
            this.hexData.Size = new System.Drawing.Size(66, 20);
            this.hexData.TabIndex = 6;
            this.hexData.TextChanged += new System.EventHandler(this.hexData_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Hex";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "UInt32";
            // 
            // intData
            // 
            this.intData.Location = new System.Drawing.Point(9, 72);
            this.intData.Name = "intData";
            this.intData.Size = new System.Drawing.Size(128, 20);
            this.intData.TabIndex = 9;
            this.intData.TextChanged += new System.EventHandler(this.intData_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Float";
            // 
            // floatData
            // 
            this.floatData.Location = new System.Drawing.Point(9, 112);
            this.floatData.Name = "floatData";
            this.floatData.Size = new System.Drawing.Size(128, 20);
            this.floatData.TabIndex = 11;
            this.floatData.TextChanged += new System.EventHandler(this.floatData_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.floatData);
            this.groupBox1.Controls.Add(this.hexData);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.intData);
            this.groupBox1.Location = new System.Drawing.Point(456, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 145);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.floatDataOrig);
            this.groupBox2.Controls.Add(this.hexDataOrig);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.intDataOrig);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(456, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 145);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Original Data";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Hex";
            // 
            // floatDataOrig
            // 
            this.floatDataOrig.Location = new System.Drawing.Point(9, 112);
            this.floatDataOrig.Name = "floatDataOrig";
            this.floatDataOrig.Size = new System.Drawing.Size(128, 20);
            this.floatDataOrig.TabIndex = 11;
            // 
            // hexDataOrig
            // 
            this.hexDataOrig.Location = new System.Drawing.Point(9, 33);
            this.hexDataOrig.Name = "hexDataOrig";
            this.hexDataOrig.Size = new System.Drawing.Size(66, 20);
            this.hexDataOrig.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Float";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "UInt32";
            // 
            // intDataOrig
            // 
            this.intDataOrig.Location = new System.Drawing.Point(9, 72);
            this.intDataOrig.Name = "intDataOrig";
            this.intDataOrig.Size = new System.Drawing.Size(128, 20);
            this.intDataOrig.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 414);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.valueList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RDLParamEdit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox fileList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox valueList;
        private System.Windows.Forms.TextBox hexData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox intData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox floatData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox floatDataOrig;
        private System.Windows.Forms.TextBox hexDataOrig;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox intDataOrig;
        private System.Windows.Forms.ToolStripMenuItem updateLabelsToolStripMenuItem;
    }
}

