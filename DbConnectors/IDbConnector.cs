using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataImporter.DbConnectors
{
    /// <summary>
    /// An interface for all the json based db connectors (mongodb, elasticsearch etc...)
    /// </summary>
    internal interface IDbConnector
    {
        void InsertDocument(object document);
    }
}
