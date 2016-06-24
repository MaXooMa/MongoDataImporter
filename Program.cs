using System;
using System.IO;
using System.Threading;
using MongoDataImporter.DataFile;
using MongoDataImporter.DbConnectors;
using MongoDataImporter.Source;


namespace MongoDataImporter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello welcome to Mongo data importer.");
            CreateTempDirectory();

            try
            {
                var connection = new MongoConnector(UserInterface.GetConnectionString(), UserInterface.GetDbName(),
                    UserInterface.GetCollectionName());
                StartThreads(UserInterface.GetSources(), connection);
            }
            catch (Exception)
            {
                Console.WriteLine("Error Connectiong to DB. Make sure the connection string is ok and try again.");
            }

            //DeleteTempDirectory();
            Console.ReadKey();
        }

        /// <summary>
        /// The method gets the list of sources and start the threads accordingly
        /// </summary>
        /// <param name="sourcesList">the string that represents the source.</param>
        /// <param name="db">IDbConnector that the file will be inserted into.</param>
        public static void StartThreads(string[] sourcesList, IDbConnector db)
        {
            if (sourcesList.Length > 1)
            {
                foreach (var source in sourcesList)
                {
                    var sourceThread = new Thread(() => ProcessSource(source, db));
                    sourceThread.Start();
                }
            }
            else
            {
                ProcessSource(sourcesList[0], db);
            }
        }

        /// <summary>
        /// The actual working method.
        /// The method gets a Source object, creates a DataFile from it
        /// and than inserts the file to the given DB.
        /// </summary>
        /// <param name="sourceStr">the string that represents the source.</param>
        /// <param name="db">IDbConnector that the file will be inserted into.</param>
        public static void ProcessSource(string sourceStr, IDbConnector db)
        {
            Console.WriteLine($@"Starting to work on the source {sourceStr}");

            var source = sourceStr.StartsWith("http") ? (Source.Source)new UrlSource(sourceStr) : new LocalSource(sourceStr);
            try
            {
                IDataFile currFile = source.GetWorkingDataFile();
                currFile.InsertFile(db);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"There was an error processing the following source: {source.SourceString}.");
                Console.WriteLine(e);
            }
            Console.WriteLine($@"Done working on the source {source.SourceString}");
        }

        internal static void CreateTempDirectory()
        {
            if (!Directory.Exists("./Temp"))
            {
                System.IO.Directory.CreateDirectory("./Temp");
            }
        }
    }
}
