using MongoDataImporter.DbConnectors;

namespace MongoDataImporter.DataFile
{
    /// <summary>
    /// An Interface that represents a datafile.
    /// A data file class represents a clean uncompressed file that has data in it that needs
    /// to be inserted.
    /// </summary>
    internal interface IDataFile
    {
        void InsertFile(IDbConnector db);
    }
}
