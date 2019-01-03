using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic.Pages
{
    public class DataPage : Page
    {
        public class DataPageHeader
        {
            public ushort FreeSpace { get; set; }
            public ulong Owner { get; set; }
            public ushort RecordCount { get; set; }
            public List<uint> RecordOffsets { get; set; }
        }

        public DataPageHeader Header { get; set; }
    }
}
