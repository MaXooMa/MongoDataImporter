using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO use enum for type
namespace MongoDataImporter.DataFile.Cell
{
    abstract class BaseCell
    {
        public string Header { get; set; }
        public string Type { get; set; }
        public string InitialValue { get; protected set; }
        public object ConvertedValue { get; protected set; }

        protected void SetInfoFromHeader()
        {
            var l = Header.IndexOf("(");
            InitialValue =  Header.Substring(0, l);

        }
    }
}
