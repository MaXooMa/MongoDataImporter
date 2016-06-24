using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace MongoDataImporter.Compressed
{
    /// <summary>
    /// An implementation if ICompressed, a class that decompresses Gziped files.
    /// </summary>
    public class Gzipped : ICompressed
    {
        /// <summary>
        /// The method that does the decompression
        /// </summary>
        public void ExtractFile(string compressedSource,string uncompressedDestination)
        {
            using (var fInStream = new FileStream(compressedSource, FileMode.Open, FileAccess.Read))
            {
                using (var zipStream = new GZipStream(fInStream, CompressionMode.Decompress))
                {
                    using (var fOutStream =
                      new FileStream(uncompressedDestination, FileMode.Create, FileAccess.Write))
                    {
                        var tempBytes = new byte[4096];
                        int i;
                        while ((i = zipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                        {
                            fOutStream.Write(tempBytes, 0, i);
                        }
                    }
                }
            }
        }
    }
}
