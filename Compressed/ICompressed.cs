using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataImporter.Compressed
{
    /// <summary>
    /// The interface of compressed files.
    /// </summary>
    internal interface ICompressed
    {
        void ExtractFile(string compressedSource, string uncompressedDestination);
    }
}
