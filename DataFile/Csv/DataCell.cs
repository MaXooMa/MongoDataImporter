using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataImporter.DataFile.Csv
{
    /// <summary>
    /// Rperesents a cell in the Csv
    /// </summary>
    internal class DataCell
    {
        public string Header { get; set; } // The full header, i.e fist_name(string)
        public string HeaderValue { get; protected set; } //The value of the header without the type, i.e first_name
        public string Type { get; set; } // A string that represents the type. string,int,datetime..
        public string InitialValue { get; protected set; } // The value of the cell before conversion.
        public object ConvertedValue { get; protected set; } //The final value of the cell after conversion

        //A dictionary the hold the appropriate method that is used to convert each type.
        private static Dictionary<string, Action<string, DataCell>> Converters = new Dictionary<string, Action<string, DataCell>>
        {
            {"int", (value, cell) => cell.ConvertedValue = int.Parse(value)},
            {"datetime", (value, cell) =>
                cell.ConvertedValue = DateTime.ParseExact(value, "d/M/yyyy", CultureInfo.InvariantCulture)},
            {"string", (value, cell) =>  cell.ConvertedValue = value}
        };


        public DataCell(string initialValue , string header)
        {
            InitialValue = initialValue;
            Header = header;
            SetInfoFromHeader();
            Convert();
        }

        /// <summary>
        /// Converts the Value of the cell to the appropriate type.
        /// </summary>
        protected void Convert()
        {
            Converters[Type](InitialValue, this);
        }

        protected void SetInfoFromHeader()
        {
            HeaderValue = Header.Split('(', ')')[0];
            Type = Header.Split('(', ')')[1];
        }
    }
}
