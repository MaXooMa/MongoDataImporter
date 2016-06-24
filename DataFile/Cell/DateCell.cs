using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataImporter.DataFile.Cell
{
    class DateCell: BaseCell
    {
        public DateCell(string intialValue, string header)
        {
            this.Header = header;
            ConvertedValue = DateTime.Parse(intialValue);
        }
    }
}
