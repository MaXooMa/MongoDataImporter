using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MongoDataImporter.DataFile.Csv
{
    /// <summary>
    /// A class that represents a dcoument in a MongoDB Collection.
    /// </summary>
    public class MongoDocument
    {
        public ObjectId Id { get; set; }
        [BsonExtraElements]
        public Dictionary<string, object> Values { get; set; }
    }

    /// <summary>
    /// The class Represents a row in the Csv data file.
    /// The main purpose of the CsvRow is to hold a list of DataCells,
    /// and to finally return an instance of MongoDocument that represents the row,
    /// when the CreateDocumentFromRow() Method is invoked.
    /// </summary>
    internal class CsvRow
    {
        private string RowString { get; set; } // The actual string that is the line the csv
        private string HeadersRowString { get; set; } //The actual string of the headers in the csv
        private List<DataCell> CellsList; // List of the cells that are in the row

        public CsvRow(string rowString, string headersRowString)
        {
            this.RowString = rowString;
            this.HeadersRowString = headersRowString;
            CreateCellsList();
        }

        /// <summary>
        /// The method splits both the header row and the data row
        /// and than create a DataCell with values from each list in the specified index.
        /// for exemple:
        ///                     0            1
        ///  headersList = [name(string),age(int)]
        /// 
        ///                   0   1
        ///  rawCellList = ["max",21]
        ///
        /// The method will pair beween headersList[0] and raw CellList [0] and so on...
        /// and the end result is : [DataCell(rawCellList [0],headersList[0])
        ///                         DataCell(rawCellList [1],headersList[1])] 
        /// 
        ///  # The header that is sent to the constructor of the cell is with the type in the parenthsis, example: first_name(string)
        /// </summary>
        private void CreateCellsList()
        {
            var headersList = HeadersRowString.Split(',');
            var rawCellsList = RowString.Split(',');

            var cellsList = rawCellsList.Select((t, i) => new DataCell(t, headersList[i])).ToList();
            CellsList = cellsList;
        }

        /// <summary>
        /// The function creates a dictionary from the CellsList
        /// example: 
        ///     {first_name: "dani",
        ///      age, 11}
        /// </summary>
        /// <returns>A MongoDocument object that contains the dictionary that we've created.</returns>
        public MongoDocument CreateDocumentFromRow()
        {
            var dict = new Dictionary<string, object>();
            foreach (var cell in CellsList)
            {
                dict.Add(cell.HeaderValue, cell.ConvertedValue);
            }

            return new MongoDocument
            {
                Id = ObjectId.GenerateNewId(),
                Values = dict
            };
        }
    }
}
