using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace AudioConv
{
    public class UtiliTunes
    {
        WebRequest request;

        public static Bitmap SearchImageFile(ImageRepo source, string searchQuery)
        {
            switch (source)
            {
                case ImageRepo.Apple:
                    return SearchApple(searchQuery);
            }

            return null;
        }

        public static string GetImageFile(string imageLink)
        {
            return "";
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

        public enum ImageRepo
        {
            Apple
        };
    }
}
