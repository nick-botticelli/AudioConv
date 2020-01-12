using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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

            Util.AudioMetadata metadata = Util.GetAlbum(file);

            StringBuilder outputError = new StringBuilder();
            Process proc1 = new Process();
            ProcessStartInfo psi1 = new ProcessStartInfo();
            psi1.FileName = @proc;
            psi1.RedirectStandardError = true;
            psi1.UseShellExecute = false;
            psi1.CreateNoWindow = true;
            psi1.ErrorDialog = false;
            psi1.WindowStyle = ProcessWindowStyle.Hidden;
            psi1.Arguments = GenerateArgs(proc, file, codec, container, bitrate, encodeImage, metadata);
            proc1.StartInfo = psi1;
            proc1.ErrorDataReceived += (s, e) => outputError.Append(e.Data);
            proc1.Start();
            proc1.BeginErrorReadLine();
            proc1.WaitForExit();

            string error = outputError.ToString();

            GC.Collect(); // Clean up garbage like album art

            // Fetch & apply metadata
            /*var anno = MusicBrainz.Search.Annotation(entity: "bdb24cb5-404b-4f60-bba4-7b730325ae47");
            var area = MusicBrainz.Search.Area("germany", type: "Country");
            var artist = MusicBrainz.Search.Artist("fred");
            var cd = MusicBrainz.Search.CdStub(title: "Doo");
            var fdb = MusicBrainz.Search.Freedb(discid: "6e108c07");
            var lab = MusicBrainz.Search.Label("Devils");
            var place = MusicBrainz.Search.Place("Studio");
            var recording = MusicBrainz.Search.Recording("Fred");
            var release = MusicBrainz.Search.Release("Schneider");
            var releaseGroup = MusicBrainz.Search.ReleaseGroup("Tenance");
            var work = MusicBrainz.Search.Work("Devils");
            var test = MusicBrainz.Search.Release("");*/

            // Search a CD with a Song from a specific Artist
            //var rec = MusicBrainz.Search.Recording(artist: metadata.artist, release: metadata.album, tracks: metadata.title);
            var rec = MusicBrainz.Search.Recording("artist:(" + metadata.artist + ") release:(" + metadata.album + ") track:(" + metadata.title + ")");
            foreach (var s in rec.Data)
            {
                MusicBrainz.Data.RecordingRelease match = null; // Find fist release in match, use it if US release is not present

                foreach (var r in s.Releaselist)
                {
                    if (r.Id == string.Empty) continue;

                    if (match == null) // Use first country by default
                        match = r;
                    else if (r.Country == "US") // Use US if present
                        match = r;
                    else if (!match.Country.ToUpper().Equals("US") && r.Country.ToUpper().Equals("XW")) // Use XW if US not present
                        match = r;
                }
            }

            this.BeginInvoke(new Action(() =>
            {
                labelThreadCount.Text = "" + --threadCount;
            }));
        }

        private string GenerateArgs(string encoder, string file, string codec, string container, int bitrate, bool encodeImage, Util.AudioMetadata metadata)
        {
            string filePath = Util.SearchImageFile(Util.ImageRepo.Apple, metadata.albumArtist + " " + metadata.album, encodeImage);
            bool artExists = filePath != null && filePath.Length > 0;

            switch (encoder.ToLower().Trim())
            {
                case "qaac":
                case "qaac64":
                    return "-q 2 -v " + bitrate + " -o \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container + "\" "
                        + (artExists ? "--artwork \"" + filePath + "\" " : "") + "\"" + file + "\"";
                case "opusenc":
                    return "--bitrate " + bitrate + " \"" + file + "\" \"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container + "\""
                        + " --padding 0" + (artExists ? " --discard-pictures --picture \"" + filePath + "\"" : "");
                case "ffmpeg":
                    return /*"-c:a " + codec +*/ "-i \"" + file + "\" -i \"" + filePath + "\" -y -c:v copy -map 0:0 -map 1:0 -b:a " + bitrate
                        + "k -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (front)\" "
                       + "\"" + Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + container + "\"";
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
