namespace AudioConv
{
    partial class FormFfmpegCodecs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFfmpegCodecs));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBoxAlbumArt = new System.Windows.Forms.PictureBox();
            this.textBoxSearchQuery = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(242, 577);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(541, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Test iTunes Request";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // pictureBoxAlbumArt
            // 
            this.pictureBoxAlbumArt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxAlbumArt.Location = new System.Drawing.Point(260, 88);
            this.pictureBoxAlbumArt.Name = "pictureBoxAlbumArt";
            this.pictureBoxAlbumArt.Size = new System.Drawing.Size(501, 501);
            this.pictureBoxAlbumArt.TabIndex = 2;
            this.pictureBoxAlbumArt.TabStop = false;
            // 
            // textBoxSearchQuery
            // 
            this.textBoxSearchQuery.Location = new System.Drawing.Point(376, 12);
            this.textBoxSearchQuery.MaxLength = 127;
            this.textBoxSearchQuery.Name = "textBoxSearchQuery";
            this.textBoxSearchQuery.Size = new System.Drawing.Size(385, 29);
            this.textBoxSearchQuery.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Album title:";
            // 
            // FormFfmpegCodecs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 601);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearchQuery);
            this.Controls.Add(this.pictureBoxAlbumArt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFfmpegCodecs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FFmpeg Codecs";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBoxAlbumArt;
        private System.Windows.Forms.TextBox textBoxSearchQuery;
        private System.Windows.Forms.Label label1;
    }
}