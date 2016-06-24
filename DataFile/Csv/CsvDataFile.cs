using System.IO;
using MongoDataImporter.DbConnectors;

namespace MongoDataImporter.DataFile.Csv
{
    /// <summary>
    /// A Csv implementation of the DataFile
    /// </summary>
    internal class CsvDataFile: IDataFile
    {
        public string FilePath { get; private set; }

        public CsvDataFile(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// The methods insert all the data that is in the file
        /// into a db.
        /// </summary>
        /// <param name="db">a IDbConnector object that the data will be inserted into.</param>
        public void InsertFile(IDbConnector db)
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string currentLine;
                string headers = sr.ReadLine(); //The full line of the header in the csv(first line)

                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    //A MongoDocument object that is created from CsvRow
                    MongoDocument doc = new CsvRow(currentLine, headers).CreateDocumentFromRow();
                    db.InsertDocument(doc);
                }
            }
        }
    }
}
