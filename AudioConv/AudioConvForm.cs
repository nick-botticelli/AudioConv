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
            textBoxStatus.Text = "Starting threads...";

            string encoder = comboBoxEncoder.Text;
            string codec = comboBoxCodec.Text;
            string container = comboBoxContainer.Text;
            int bitrate = (int) numericUpDownBitrate.Value;
            bool encodeImage = checkBoxEncodeImage.Checked;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                // Central thread for launching other threads!
                // Should keep UI thread open, for even more drops!
                // Which means more central threads to spawn even more threads!
                Thread thread = new Thread(() =>
                {
                    bool async = false;
                    foreach (string file in files)
                    {
                        StartEncoder(encoder, file, codec, container, bitrate, encodeImage, async);
                        async = true;
                    }
                });

                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }

            textBoxStatus.Text = "Done starting threads.";
        }

        public void StartEncoder(string proc, string file, string codec, string container, int bitrate, bool encodeImage, bool threaded)
        {
            if (threaded)
            {
                Thread thread = new Thread(() =>
                {
                    StartEncoderLogic(proc, file, codec, container, bitrate, encodeImage);
                });

                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                StartEncoderLogic(proc, file, codec, container, bitrate, encodeImage);
            }
        }

        private void StartEncoderLogic(string proc, string file, string codec, string container, int bitrate, bool encodeImage)
        {
            // Interact with UI thread
            this.BeginInvoke(new Action(() =>
            {
                labelThreadCount.Text = "" + ++threadCount;
            }));

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

            GC.Collect(); // Clean up garbage like album art

            this.BeginInvoke(new Action(() =>
            {
                labelThreadCount.Text = "" + --threadCount;
            }));
        }

        private string GenerateArgs(string encoder, string file, string codec, string container, int bitrate, bool encodeImage)
        {
            switch (encoder.ToLower().Trim())
            {
                case "qaac":
                    return "-q 2 - v " + bitrate + " - o \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container + "\" \"" + file + "\"";
                case "opusenc":
                    Util.AudioMetadata metadata = Util.GetAlbum(file);
                    string filePath = Util.SearchImageFile(Util.ImageRepo.Apple, metadata.albumArtist + " " + metadata.album, encodeImage);

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

        private void FormAudioConv_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(Util.TEMP_PATH))
                try { Directory.Delete(Util.TEMP_PATH, true); } catch (Exception ignored) { } // Clean up files since cache is lost on program exit
        }
    }
}
