using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic.Pages
{
    public class DataPage : Page
    {
        public class RecordOffset
        {
            public ushort Data { get; set; }
            public bool Ignore
            {
                get => (this.Data & 0x8000) == 0x8000;
            }

            public bool Overflow
            {
                get => (this.Data & 0x4000) == 0x4000;
            }

            public ushort Offset
            {
                get => (ushort)(this.Data & 0x0FFF);
            }

            public RecordOffset(ushort data)
            {
                this.Data = data;
            }
        }

        public class DataPageHeader
        {
            public bool LVAL { get; set; }
            public ushort FreeSpace { get; set; }
            public ulong Owner { get; set; }
            public ushort RecordCount { get; set; }
            public List<RecordOffset> RecordOffsets { get; set; }
        }

        public DataPage(byte[] data) : base(data)
        {
            this.LoadHeader(data);
        }

        public DataPageHeader Header { get; set; }
    }
}
