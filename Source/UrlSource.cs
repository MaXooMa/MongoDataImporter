using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MongoDataImporter.DataFile;

namespace MongoDataImporter.Source
{
    internal class UrlSource: Source
    {
        public UrlSource(string sourceString) : base(sourceString) { }

        /// <summary>
        /// Try to send a request before downlaoding in order to see that this is a valid and working url.
        /// </summary>
        /// <returns></returns>
        public override bool IsValidSource()
        {
            DeleteLastSlash();
            try
            {
                HttpWebRequest request = WebRequest.Create(SourceString) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will return false.
                return false;
            }
        }

        public override string CreateTempCopy()
        {
            
            var fileName = Path.GetFileName(SourceString);
            string tempPath = $@"./Temp/{fileName}";
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(SourceString, tempPath);
            }
            return tempPath;
        }

    }
}
