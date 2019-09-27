using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace AudioConv
{
    public class Util
    {
        public static readonly string TEMP_PATH = Path.GetTempPath() + "AudioConv" + Path.DirectorySeparatorChar;
        private static readonly int RESIZE_THRESHOLD = 1250;

        private static WebRequest request;
        private static Dictionary<string, string> artCache = new Dictionary<string, string>();

        public static Bitmap SearchImage(ImageRepo source, string searchQuery, bool encodeImage)
        {
            string filePath = SearchImageFile(source, searchQuery, encodeImage);

            return new Bitmap(filePath);
        }

        public static string SearchImageFile(ImageRepo source, string searchQuery, bool encodeImage)
        {
            string cachePath = "";
            artCache.TryGetValue(searchQuery, out cachePath);

            if (cachePath != null && cachePath.Length > 0)
                return cachePath;

            if (!Directory.Exists(Path.GetTempPath() + "AudioConv"))
                Directory.CreateDirectory(TEMP_PATH);

            Bitmap bitmap = null;
            string filePath = "";
            switch (source)
            {
                case ImageRepo.Apple:
                    bitmap = SearchApple(searchQuery);
                    filePath = TEMP_PATH + Path.DirectorySeparatorChar + Guid.NewGuid().ToString() + ".png"; // Generate unique name
                    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    try { artCache.Add(searchQuery, filePath); } catch (Exception ignored) { } // Fix threading crash
                    break;
            }

            if (encodeImage && filePath != null && filePath.Length > 0 && bitmap != null)
            {
                // Encode to targa with FFmpeg and resize to 1000x1000 with Lanczos (if smallest width > 1250)
                Process proc1 = new Process();
                ProcessStartInfo psi1 = new ProcessStartInfo();
                psi1.FileName = @"ffmpeg";
                psi1.UseShellExecute = true;
                psi1.CreateNoWindow = true;
                psi1.ErrorDialog = false;
                psi1.WindowStyle = ProcessWindowStyle.Hidden;
                psi1.Arguments = "-i \"" + filePath + "\" " + (Math.Min(bitmap.Width, bitmap.Height) > RESIZE_THRESHOLD ? "-vf scale=1000:-1 -sws_flags lanczos+full_chroma_inp+full_chroma_int+accurate_rnd " : "") + "\"" + TEMP_PATH + Path.GetFileNameWithoutExtension(filePath) + ".tga\"";
                proc1.StartInfo = psi1;
                proc1.Start();
                proc1.WaitForExit();

                // Delete original PNG
                File.Delete(filePath);

                // Encode to jpg with mozjpeg
                Process proc2 = new Process();
                ProcessStartInfo psi2 = new ProcessStartInfo();
                psi2.FileName = @"cjpeg";
                psi2.UseShellExecute = true;
                psi2.CreateNoWindow = true;
                psi2.ErrorDialog = false;
                psi2.WindowStyle = ProcessWindowStyle.Hidden;
                psi2.Arguments = "-q 80 -outfile \"" + TEMP_PATH + Path.GetFileNameWithoutExtension(filePath) + ".jpg\" \"" + TEMP_PATH + Path.GetFileNameWithoutExtension(filePath) + ".tga\"";
                proc2.StartInfo = psi2;
                proc2.Start();
                proc2.WaitForExit();

                // Delete Targa encode
                File.Delete(TEMP_PATH + Path.GetFileNameWithoutExtension(filePath) + ".tga");

                artCache.Remove(searchQuery);
                artCache.Add(searchQuery, TEMP_PATH + Path.GetFileNameWithoutExtension(filePath) + ".jpg");

                return TEMP_PATH + Path.GetFileNameWithoutExtension(filePath) + ".jpg";
            }

            return filePath;
        }

        public static Bitmap GetImageFromCache(string searchQuery)
        {
            string path = "";
            artCache.TryGetValue(searchQuery, out path);

            if (path.Length == 0)
                return null;

            return new Bitmap(path);
        }

        private static Bitmap SearchApple(string searchQuery)
        {
            // Initial search request
            string searchSkeleton = @"https://itunes.apple.com/search?country=us&entity=album&limit=25&term=" + WebUtility.UrlEncode(searchQuery);

            WebRequest request = WebRequest.Create(searchSkeleton);
            request.Proxy = null;

            WebResponse response = request.GetResponse();
            StringBuilder sb = new StringBuilder();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                sb.Append(reader.ReadToEnd());
            }

            response.Close();

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(sb.ToString());
            string result = dobj["results"][0]["artworkUrl100"].ToString().Replace("100x100bb.jpg", "99999x99999-999.jpg");

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(result);
            Bitmap bitmap = new Bitmap(stream);

            stream.Flush();
            stream.Close();
            client.Dispose();

            return bitmap;
        }

        static void DoWithResponse(HttpWebRequest request, Action<HttpWebResponse> responseAction)
        {
            Action wrapperAction = () =>
            {
                request.BeginGetResponse(new AsyncCallback((iar) =>
                {
                    var response = (HttpWebResponse)((HttpWebRequest)iar.AsyncState).EndGetResponse(iar);
                    responseAction(response);
                }), request);
            };
            wrapperAction.BeginInvoke(new AsyncCallback((iar) =>
            {
                var action = (Action)iar.AsyncState;
                action.EndInvoke(iar);
            }), wrapperAction);
        }

        public static AudioMetadata GetAlbum(string audioFile)
        {
            Process proc1 = new Process();
            ProcessStartInfo psi1 = new ProcessStartInfo();
            StringBuilder output = new StringBuilder(), outputError = new StringBuilder();

            psi1.FileName = @"ffprobe";
            psi1.RedirectStandardOutput = true;
            psi1.RedirectStandardError = true;
            psi1.UseShellExecute = false;
            psi1.CreateNoWindow = true;
            psi1.ErrorDialog = false;
            psi1.WindowStyle = ProcessWindowStyle.Hidden;
            psi1.Arguments = "-v quiet -print_format json=compact=1 -show_format \"" + audioFile + "\"";
            proc1.StartInfo = psi1;
            proc1.OutputDataReceived += (s, e) => output.Append(e.Data);
            proc1.ErrorDataReceived += (s, e) => outputError.Append(e.Data);
            proc1.Start();
            proc1.BeginOutputReadLine();
            proc1.BeginErrorReadLine();
            proc1.WaitForExit();

            if (outputError.Length > 0)
                return new AudioMetadata(outputError.ToString());

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(output.ToString());
            dynamic result = dobj["format"]["tags"];

            string artist = result["ARTIST"], album = result["ALBUM"];

            AudioMetadata metadata = new AudioMetadata(artist, result["TITLE"], album);
            metadata.albumArtist = result["album_artist"];

            return metadata;
        }

        public class AudioMetadata
        {
            public string artist, title, album, albumArtist, track, tracktotal, disc, discTotal, genre,
                isrc, length, barcode, copyright, date, bpm, publisher;

            public string error;

            public AudioMetadata(string artist, string title, string album)
            {
                this.artist = artist;
                this.albumArtist = artist;
                this.title = title;
                this.album = album;
            }

            public AudioMetadata(string error)
            {
                this.error = error;
            }
        }

        public enum ImageRepo
        {
            Apple
        };
    }
}
