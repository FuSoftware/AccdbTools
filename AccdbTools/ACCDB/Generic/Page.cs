using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic
{
    public enum PageType
    {
        Header = 0x0100,
        Data = 0x0101,
        TableDefinition = 0x0102,
        IntermediateIndex = 0x0103,
        LeafIndex = 0x0104,
		PageUsageBitmaps = 0x0105,
        Unknown1 = 0x0108,
    }

    public class Page
    {
        public PageType PageSignature { get; set; }
        public byte[] PageData { get; set; }

        public Page(byte[] data)
        {
            this.PageData = data;
        }

        public void LoadHeader()
        {
            this.LoadHeader(this.PageData);
        }

        public virtual void LoadHeader(byte[] data)
        {

        }

        public void Load()
        {
            this.Load(this.PageData);
        }

        public virtual void Load(byte[] data)
        {

        }

        public void ClearData()
        {
            this.PageData = new byte[0];
        }

        public static ushort DataPageType(byte[] fileData, int pageIndex, int pageLength = 4096)
        {
            return BitConverter.ToUInt16(fileData, pageIndex * pageLength);
        }

        public static List<ushort> DataPageTypeList(byte[] fileData, int pageLength = 4096)
        {
            List<ushort> values = new List<ushort>();

            for (int i=0;i<(fileData.Length/ pageLength); i++)
            {
                values.Add(DataPageType(fileData,i,pageLength));
            }

            return values;
        }
    }
}
