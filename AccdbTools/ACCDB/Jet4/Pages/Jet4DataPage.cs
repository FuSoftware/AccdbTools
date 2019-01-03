using AccdbTools.ACCDB.Generic;
using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet4.Pages
{
    public class Jet4DataPage : DataPage
    {
        public class Jet4DataPageHeader : DataPageHeader
        {
            public uint A1 { get; set; }

            public Jet4DataPageHeader(byte[] data)
            {
                this.Load(data);
            }

            public void Load(byte[] data)
            {
                this.FreeSpace = BitConverter.ToUInt16(data, 2);
                this.Owner = BitConverter.ToUInt32(data, 4);
                this.A1 = BitConverter.ToUInt32(data, 8);
                this.RecordCount = BitConverter.ToUInt16(data, 12);

                this.RecordOffsets = new List<uint>();

                for(uint i = 0; i < this.RecordCount; i++)
                {
                    this.RecordOffsets.Add(BitConverter.ToUInt16(data, 12 + (int)i*2));
                }
            }
        }

        public Jet4DataPage(byte[] data)
        {
            this.Load(data);
        }

        public void Load(byte[] data)
        {
            this.PageSignature = (PageType)BitConverter.ToUInt16(data, 0);
            this.Header = new Jet4DataPageHeader(data);
        }
    }
}
