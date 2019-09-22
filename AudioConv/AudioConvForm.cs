using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AudioConv
{
    public partial class FormAudioConv : Form
    {
        Random rand;

        public FormAudioConv()
        {
            InitializeComponent();
        }

        private void FormAudioConv_Load(object sender, EventArgs e)
        {
            rand = new Random();
        }

        private void FormAudioConv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void comboBoxCodec_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelBitrate.Enabled = !(comboBoxCodec.Text == "FLAC" || comboBoxCodec.Text == "ALAC");
            numericUpDownBitrate.Enabled = !(comboBoxCodec.Text == "FLAC" || comboBoxCodec.Text == "ALAC");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Opacity = (checkBoxTransparent.Checked) ? .50 : 1.0;
        }

        private void FormAudioConv_DragDrop(object sender, DragEventArgs e)
        {
            textBoxStatus.Text = "Converting...";

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    StartEncoder(comboBoxEncoder.Text, file);

                    /*if (comboBoxCodec.Text.Equals("AAC (qaac64)"))
                    {
                        //MessageBox.Show("Is AAC!\n\n" + file + "\n\n" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text);

                        var enviromentPath = Environment.GetEnvironmentVariable("PATH");
                        
                        Process proc1 = new Process();
                        ProcessStartInfo psi1 = new ProcessStartInfo();
                        psi1.FileName = @"qaac64.exe";
                        psi1.UseShellExecute = true;
                        psi1.CreateNoWindow = true;
                        psi1.ErrorDialog = false;
                        psi1.WindowStyle = ProcessWindowStyle.Hidden;
                        psi1.Arguments = "-q 2 -v " + (int)numericUpDownBitrate.Value + " -o \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text + "\" \"" + file + "\"";
                        proc1.StartInfo = psi1;

                        proc1.Start();

                        textBoxStatus.ForeColor = Color.FromArgb((int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250));
                    }
                    else if (comboBoxCodec.Text.Equals("Opus"))
                    {
                        var enviromentPath = Environment.GetEnvironmentVariable("PATH");

                        Process proc1 = new Process();
                        ProcessStartInfo psi1 = new ProcessStartInfo();
                        psi1.FileName = @"opusenc.exe";
                        psi1.UseShellExecute = true;
                        psi1.CreateNoWindow = true;
                        psi1.ErrorDialog = false;
                        psi1.WindowStyle = ProcessWindowStyle.Hidden;
                        psi1.Arguments = "--bitrate " + (int)numericUpDownBitrate.Value + " \"" + file + "\" \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text + "\"";
                        proc1.StartInfo = psi1;

                        proc1.Start();

                        textBoxStatus.ForeColor = Color.FromArgb((int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250));
                    }
                    else
                    {
                        MessageBox.Show("Not AAC!\n\n" + file + "\n\n" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text);

                        Process proc = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "ffmpeg.exe";
                        //psi.CreateNoWindow = true;
                        //psi.ErrorDialog = false;
                        //psi.WindowStyle = ProcessWindowStyle.Hidden;
                        psi.Arguments = "-i \"" + file + "\" ";
                        if (comboBoxContainer.Text == ".flac" || comboBoxContainer.Text == ".alac" || comboBoxContainer.Text == ".aiff" || comboBoxContainer.Text == ".wav")
                            if (Path.GetExtension(file) == ".flac" || Path.GetExtension(file) == ".alac" || Path.GetExtension(file) == ".aiff" || Path.GetExtension(file) == ".wav")
                                psi.Arguments += "-c copy ";
                            else { }
                        else
                            psi.Arguments += "-b:a " + (int)numericUpDownBitrate.Value;
                        psi.Arguments += "-c:a " + comboBoxCodec.Text + "k \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text + "\"";

                        proc.StartInfo = psi;

                        proc.Start();

                        textBoxStatus.ForeColor = Color.FromArgb((int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250));
                    }*/
                }
            }

            textBoxStatus.Text = "Done!";
        }

        public bool StartEncoder(string proc, string file)
        {
            var enviromentPath = Environment.GetEnvironmentVariable("PATH");

            Process proc1 = new Process();
            ProcessStartInfo psi1 = new ProcessStartInfo();
            psi1.FileName = @proc;
            psi1.UseShellExecute = true;
            psi1.CreateNoWindow = true;
            psi1.ErrorDialog = false;
            psi1.WindowStyle = ProcessWindowStyle.Hidden;
            psi1.Arguments = GenerateArgs(proc, file);
            proc1.StartInfo = psi1;

            textBoxStatus.ForeColor = Color.FromArgb((int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250), (int)(rand.NextDouble() * 250));

            return proc1.Start();
        }

        private string GenerateArgs(string encoder, string file)
        {
            switch (encoder)
            {
                case "qaac":
                    return "-q 2 - v " + (int)numericUpDownBitrate.Value + " - o \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text + "\" \"" + file + "\"";
                case "opusenc":
                    return "--bitrate " + (int)numericUpDownBitrate.Value + " \"" + file + "\" \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + comboBoxContainer.Text + "\"";
            }

            // FFmpeg

            switch (comboBoxCodec.Text.Substring(0, encoder.IndexOf('(')).ToLower().Trim())
            {
                case "opus":
                    return "-c:a libopus";
            }

            return "";
        }

        private void ButtonFfmpegCodecs_Click(object sender, EventArgs e)
        {
            new FormFfmpegCodecs().Show();
        }
    }
}
