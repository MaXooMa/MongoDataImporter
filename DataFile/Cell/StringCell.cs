using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataImporter.DataFile.Cell
{
    class StringCell: BaseCell
    {
        public StringCell(string initialValue, string header)
        {
            this.Header = header;
            this.ConvertedValue = initialValue;
        }
    }
}
