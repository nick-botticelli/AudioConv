using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AudioConv
{
    public partial class FormAudioConv : Form
    {
        Random random;
        private int threadCount;

        public FormAudioConv()
        {
            InitializeComponent();
        }

        private void FormAudioConv_Load(object sender, EventArgs e)
        {
            random = new Random();
            threadCount = 0;
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

            string encoder = comboBoxEncoder.Text;
            string codec = comboBoxCodec.Text;
            string container = comboBoxContainer.Text;
            int bitrate = (int) numericUpDownBitrate.Value;
            bool encodeImage = checkBoxEncodeImage.Checked;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    StartEncoder(encoder, file, codec, container, bitrate, encodeImage);

                    /*else
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

        public void StartEncoder(string proc, string file, string codec, string container, int bitrate, bool encodeImage)
        {
            /*labelThreadCount.Text = "" + ++threadCount;

            //string enviromentPath = Environment.GetEnvironmentVariable("PATH");

            Process proc1 = new Process();
            ProcessStartInfo psi1 = new ProcessStartInfo();
            psi1.FileName = @proc;
            psi1.UseShellExecute = true;
            psi1.CreateNoWindow = true;
            psi1.ErrorDialog = false;
            psi1.WindowStyle = ProcessWindowStyle.Hidden;
            psi1.Arguments = GenerateArgs(proc, file, codec, container, bitrate);
            proc1.StartInfo = psi1;
            proc1.Start();
            proc1.WaitForExit();

            labelThreadCount.Text = "" + --threadCount;*/

            // TODO: Fix asynchronous encoding
            Thread thread = new Thread(() =>
            {
                // Interact with UI thread
                this.BeginInvoke(new Action(() =>
                {
                    labelThreadCount.Text = "" + ++threadCount;
                }));

                //string enviromentPath = Environment.GetEnvironmentVariable("PATH");

                Process proc1 = new Process();
                ProcessStartInfo psi1 = new ProcessStartInfo();
                psi1.FileName = @proc;
                psi1.UseShellExecute = true;
                psi1.CreateNoWindow = true;
                psi1.ErrorDialog = false;
                psi1.WindowStyle = ProcessWindowStyle.Hidden;
                psi1.Arguments = GenerateArgs(proc, file, codec, container, bitrate, encodeImage);
                proc1.StartInfo = psi1;
                proc1.Start();
                proc1.WaitForExit();

                this.BeginInvoke(new Action(() =>
                {
                    labelThreadCount.Text = "" + --threadCount;
                }));
            });

            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private string GenerateArgs(string encoder, string file, string codec, string container, int bitrate, bool encodeImage)
        {
            switch (encoder.ToLower().Trim())
            {
                case "qaac":
                    return "-q 2 - v " + bitrate + " - o \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container + "\" \"" + file + "\"";
                case "opusenc":
                    Util.AudioMetadata metadata = Util.GetAlbum(file);
                    string filePath = Util.SearchImageFile(Util.ImageRepo.Apple, metadata.artist + " " + metadata.album, encodeImage);

                    return "--bitrate " + bitrate + " \"" + file + "\" \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container + "\""
                        + " --padding 0 --discard-pictures --picture \"" + filePath + "\"";
                case "ffmpeg":
                    return "-c:a " + codec + " -b:a " + bitrate + "k " + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container;
            }

            return "";
        }

        private void ButtonFfmpegCodecs_Click(object sender, EventArgs e)
        {
            new FormFfmpegCodecs().Show();
        }

        private void ButtonInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
