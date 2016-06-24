using System;


namespace MongoDataImporter
{
    /// <summary>
    /// A class that holds methods that interact with the user.
    /// </summary>
    class UserInterface
    {

        public static string[] GetSources()
        {
            Console.WriteLine();
            Console.WriteLine("Insert The desiarable sources separated by commas.");
            Console.WriteLine("#Note that a url should be written with the http beginning.");
            Console.WriteLine("    Example:");
            Console.WriteLine(@"    *http://localhost/mytest.csv.gz,C:\abc.csv.gz*");
            return Console.ReadLine().ToLower().Split(',');
        }

        public static string GetConnectionString()
        {
            Console.WriteLine();
            Console.WriteLine("Insert the connection string for the db.");
            Console.WriteLine("    Example:");
            Console.WriteLine(@"    *mongodb://localhost*");
            return Console.ReadLine();
        }

        public static string GetDbName()
        {
            Console.WriteLine();
            Console.WriteLine("Insert the name of the db.");
            return Console.ReadLine();
        }

        public static string GetCollectionName()
        {
            Console.WriteLine();
            Console.WriteLine("Insert the name of the collection.");
            return Console.ReadLine();
        }
    }
}
