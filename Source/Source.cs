using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDataImporter.Compressed;
using MongoDataImporter.DataFile;
using MongoDataImporter.DataFile.Csv;

//TODO try catch

namespace MongoDataImporter.Source
{
    /// <summary>
    /// An abstract class that represents a source (currently can be a url or local source)
    /// </summary>
    internal abstract class Source
    {
        public string SourceString { get; private set; }

        public abstract bool IsValidSource(); 
        public abstract string CreateTempCopy();

        protected void DeleteLastSlash()
        {
            if (SourceString.EndsWith("\\") || SourceString.EndsWith("/"))
            {
                SourceString.Remove(SourceString.Length - 1);
            }
        }

        /// <summary>
        /// The function intializes a Gzipped object and than calls the object's ExtractFile method.
        /// </summary>
        /// <returns>The full path to the newely extracted file</returns>
        public static string ExtractFile(string compressedFileLocation)
        {
            var fileNmeWithoutExtension = Path.GetFileNameWithoutExtension(compressedFileLocation);
            string compressionDest = $@"./Temp/{fileNmeWithoutExtension}";

            var compressedFile = new Gzipped();
            compressedFile.ExtractFile(compressedFileLocation, compressionDest);

            return compressionDest;
        }

        public static bool IsCompressed(string filePath)
        {
            return (Path.GetExtension(filePath) == ".gz");
        }

        protected Source(string sourceString)
        {
            this.SourceString = sourceString;
        }

        /// <summary>
        /// A method that returns an IDataFile,
        /// This method actual does all the work that has to be done until
        /// we have a working data file (fetching from the internet or copy localy, decompessing, etc..)
        /// </summary>
        /// <returns></returns>
        public IDataFile GetWorkingDataFile()
        {
            DeleteLastSlash();
            if (IsValidSource())
            {
                var workingCopy = CreateTempCopy();
                if (IsCompressed(workingCopy))
                {
                    workingCopy = ExtractFile(workingCopy);
                }
                return new CsvDataFile(workingCopy);
            }
            else
            {
                throw new System.FormatException("Bad source format! make sure you either giva a local path or a valid url.");
            }
        }
    }
}
