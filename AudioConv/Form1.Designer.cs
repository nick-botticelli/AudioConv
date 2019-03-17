namespace AudioConv
{
    partial class FormAudioConv
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
            this.labelHeader = new System.Windows.Forms.Label();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDownBitrate = new System.Windows.Forms.NumericUpDown();
            this.labelBitrate = new System.Windows.Forms.Label();
            this.comboBoxContainer = new System.Windows.Forms.ComboBox();
            this.labelContainer = new System.Windows.Forms.Label();
            this.comboBoxCodec = new System.Windows.Forms.ComboBox();
            this.labelCodec = new System.Windows.Forms.Label();
            this.checkBoxDeleteOld = new System.Windows.Forms.CheckBox();
            this.checkBoxSamePath = new System.Windows.Forms.CheckBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.groupBoxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBitrate)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelHeader.Location = new System.Drawing.Point(63, 9);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(427, 32);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "DROP AUDIO FILE ANYWHERE";
            this.labelHeader.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormAudioConv_DragDrop);
            this.labelHeader.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormAudioConv_DragEnter);
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBox1);
            this.groupBoxOptions.Controls.Add(this.numericUpDownBitrate);
            this.groupBoxOptions.Controls.Add(this.labelBitrate);
            this.groupBoxOptions.Controls.Add(this.comboBoxContainer);
            this.groupBoxOptions.Controls.Add(this.labelContainer);
            this.groupBoxOptions.Controls.Add(this.comboBoxCodec);
            this.groupBoxOptions.Controls.Add(this.labelCodec);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteOld);
            this.groupBoxOptions.Controls.Add(this.checkBoxSamePath);
            this.groupBoxOptions.Controls.Add(this.labelPath);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 257);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(528, 211);
            this.groupBoxOptions.TabIndex = 1;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Export Options";
            this.groupBoxOptions.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormAudioConv_DragEnter);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(401, 181);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(121, 24);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Transparent";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // numericUpDownBitrate
            // 
            this.numericUpDownBitrate.Location = new System.Drawing.Point(401, 126);
            this.numericUpDownBitrate.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.numericUpDownBitrate.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
            this.numericUpDownBitrate.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownBitrate.Name = "numericUpDownBitrate";
            this.numericUpDownBitrate.Size = new System.Drawing.Size(121, 26);
            this.numericUpDownBitrate.TabIndex = 10;
            this.numericUpDownBitrate.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // labelBitrate
            // 
            this.labelBitrate.AutoSize = true;
            this.labelBitrate.Location = new System.Drawing.Point(293, 128);
            this.labelBitrate.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.labelBitrate.Name = "labelBitrate";
            this.labelBitrate.Size = new System.Drawing.Size(108, 20);
            this.labelBitrate.TabIndex = 9;
            this.labelBitrate.Text = "Bitrate (kbps):";
            // 
            // comboBoxContainer
            // 
            this.comboBoxContainer.FormattingEnabled = true;
            this.comboBoxContainer.Items.AddRange(new object[] {
            ".m4a",
            ".mp3",
            ".ogg",
            ".flac",
            ".alac",
            ".aiff",
            ".wav",
            ".mp4",
            ".webm",
            ".mkv",
            ".mov"});
            this.comboBoxContainer.Location = new System.Drawing.Point(88, 159);
            this.comboBoxContainer.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.comboBoxContainer.Name = "comboBoxContainer";
            this.comboBoxContainer.Size = new System.Drawing.Size(121, 28);
            this.comboBoxContainer.TabIndex = 8;
            this.comboBoxContainer.Text = ".ogg";
            // 
            // labelContainer
            // 
            this.labelContainer.AutoSize = true;
            this.labelContainer.Location = new System.Drawing.Point(6, 162);
            this.labelContainer.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.labelContainer.Name = "labelContainer";
            this.labelContainer.Size = new System.Drawing.Size(82, 20);
            this.labelContainer.TabIndex = 7;
            this.labelContainer.Text = "Container:";
            // 
            // comboBoxCodec
            // 
            this.comboBoxCodec.FormattingEnabled = true;
            this.comboBoxCodec.Items.AddRange(new object[] {
            "AAC (qaac64)",
            "Opus",
            "MP3",
            "Vorbis",
            "FLAC",
            "ALAC"});
            this.comboBoxCodec.Location = new System.Drawing.Point(88, 125);
            this.comboBoxCodec.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.comboBoxCodec.Name = "comboBoxCodec";
            this.comboBoxCodec.Size = new System.Drawing.Size(121, 28);
            this.comboBoxCodec.TabIndex = 6;
            this.comboBoxCodec.Text = "Opus";
            this.comboBoxCodec.SelectedIndexChanged += new System.EventHandler(this.comboBoxCodec_SelectedIndexChanged);
            // 
            // labelCodec
            // 
            this.labelCodec.AutoSize = true;
            this.labelCodec.Location = new System.Drawing.Point(29, 128);
            this.labelCodec.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.labelCodec.Name = "labelCodec";
            this.labelCodec.Size = new System.Drawing.Size(59, 20);
            this.labelCodec.TabIndex = 4;
            this.labelCodec.Text = "Codec:";
            // 
            // checkBoxDeleteOld
            // 
            this.checkBoxDeleteOld.AutoSize = true;
            this.checkBoxDeleteOld.Enabled = false;
            this.checkBoxDeleteOld.Location = new System.Drawing.Point(52, 71);
            this.checkBoxDeleteOld.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.checkBoxDeleteOld.Name = "checkBoxDeleteOld";
            this.checkBoxDeleteOld.Size = new System.Drawing.Size(131, 24);
            this.checkBoxDeleteOld.TabIndex = 3;
            this.checkBoxDeleteOld.Text = "Delete old file";
            this.checkBoxDeleteOld.UseVisualStyleBackColor = true;
            // 
            // checkBoxSamePath
            // 
            this.checkBoxSamePath.AutoSize = true;
            this.checkBoxSamePath.Checked = true;
            this.checkBoxSamePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSamePath.Enabled = false;
            this.checkBoxSamePath.Location = new System.Drawing.Point(52, 41);
            this.checkBoxSamePath.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.checkBoxSamePath.Name = "checkBoxSamePath";
            this.checkBoxSamePath.Size = new System.Drawing.Size(190, 24);
            this.checkBoxSamePath.TabIndex = 2;
            this.checkBoxSamePath.Text = "Same folder/file-name";
            this.checkBoxSamePath.UseVisualStyleBackColor = true;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(6, 42);
            this.labelPath.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(46, 20);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Path:";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStatus.Enabled = false;
            this.textBoxStatus.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStatus.Location = new System.Drawing.Point(203, 225);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(147, 23);
            this.textBoxStatus.TabIndex = 2;
            this.textBoxStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormAudioConv
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(552, 480);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormAudioConv";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "AudioConv";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormAudioConv_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormAudioConv_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormAudioConv_DragEnter);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBitrate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.CheckBox checkBoxSamePath;
        private System.Windows.Forms.CheckBox checkBoxDeleteOld;
        private System.Windows.Forms.Label labelCodec;
        private System.Windows.Forms.ComboBox comboBoxCodec;
        private System.Windows.Forms.ComboBox comboBoxContainer;
        private System.Windows.Forms.Label labelContainer;
        private System.Windows.Forms.NumericUpDown numericUpDownBitrate;
        private System.Windows.Forms.Label labelBitrate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBoxStatus;
    }
}

