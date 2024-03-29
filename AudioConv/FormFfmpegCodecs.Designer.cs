﻿namespace AudioConv
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
            this.buttonTest = new System.Windows.Forms.Button();
            this.pictureBoxAlbumArt = new System.Windows.Forms.PictureBox();
            this.textBoxSearchQuery = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(7, 7);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(134, 314);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(295, 25);
            this.buttonTest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(120, 19);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "Test iTunes Request";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.Button1_Click);
            // 
            // pictureBoxAlbumArt
            // 
            this.pictureBoxAlbumArt.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAlbumArt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxAlbumArt.Location = new System.Drawing.Point(142, 48);
            this.pictureBoxAlbumArt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxAlbumArt.Name = "pictureBoxAlbumArt";
            this.pictureBoxAlbumArt.Size = new System.Drawing.Size(273, 271);
            this.pictureBoxAlbumArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAlbumArt.TabIndex = 2;
            this.pictureBoxAlbumArt.TabStop = false;
            // 
            // textBoxSearchQuery
            // 
            this.textBoxSearchQuery.Location = new System.Drawing.Point(205, 7);
            this.textBoxSearchQuery.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxSearchQuery.MaxLength = 127;
            this.textBoxSearchQuery.Name = "textBoxSearchQuery";
            this.textBoxSearchQuery.Size = new System.Drawing.Size(212, 20);
            this.textBoxSearchQuery.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Album title:";
            // 
            // FormFfmpegCodecs
            // 
            this.AcceptButton = this.buttonTest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(422, 326);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearchQuery);
            this.Controls.Add(this.pictureBoxAlbumArt);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.PictureBox pictureBoxAlbumArt;
        private System.Windows.Forms.TextBox textBoxSearchQuery;
        private System.Windows.Forms.Label label1;
    }
}