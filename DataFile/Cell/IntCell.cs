using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataImporter.DataFile.Cell
{
    class IntCell: BaseCell
    {
        public IntCell(string initialValue, string header)
        {
            this.Header = header;
            this.ConvertedValue = int.Parse(initialValue);
        }
    }
}
