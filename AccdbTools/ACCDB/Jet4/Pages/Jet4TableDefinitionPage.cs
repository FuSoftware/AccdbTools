using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet4.Pages
{
    class Jet4Index
    {

    }

    class Jet4RealIndex : RealIndex
    {
        uint A1 { get; set; }
        uint A2 { get; set; }
        uint B1 { get; set; }
        uint B3 { get; set; }
        uint B4 { get; set; }

        public Jet4RealIndex(byte[] data)
        {
            this.Load(data);
        }

        void Load(byte[] data)
        {
            this.A1 = BitConverter.ToUInt32(data, 0);
            this.IndexRowCount = BitConverter.ToUInt32(data, 4);
            this.A2 = BitConverter.ToUInt32(data, 8);
        }

        public void LoadSecondPart(byte[] data)
        {
            this.B1 = BitConverter.ToUInt32(data, 0);

            this.Columns = new List<ColData>();
            for(int i = 0; i < 10; i++)
            {
                this.Columns.Add(
                    new ColData(
                        BitConverter.ToUInt16(data, (4+i*3)),
                        data[4+i*3+2]
                    )
                );
            }
            this.B2 = BitConverter.ToUInt32(data, 34);

            this.FirstIndexPage = BitConverter.ToUInt32(data, 38);
            this.Flags = BitConverter.ToUInt16(data, 42);
            this.B3 = BitConverter.ToUInt32(data, 44);
            this.B4 = BitConverter.ToUInt32(data, 48);
        }
    }

    class Jet4Column : Column
    {
        uint uA;
        uint uB;
    }

    class Jet4TableDefinitionPage : TableDefinitionPage
    {
        public ulong AutonumberIncrement { get; set; }
        public ulong ComplexAutonumber { get; set; }

        public Jet4TableDefinitionPage(byte[] pageData) : base(pageData)
        {
            this.Load(pageData);
        }

        public void Load(byte[] pageData)
        {
            this.PageSignature = BitConverter.ToUInt16(pageData, 0);
            this.NextPage = BitConverter.ToUInt32(pageData, 4);
            this.Length = BitConverter.ToUInt32(pageData, 8);
            this.Rows = BitConverter.ToUInt32(pageData, 16);
            this.Autonumber = BitConverter.ToUInt32(pageData, 20);
            this.AutonumberIncrement = BitConverter.ToUInt32(pageData, 24);
            this.ComplexAutonumber = BitConverter.ToUInt32(pageData, 28);
            this.TableFlag = pageData[40];
            this.NextColumnId = BitConverter.ToUInt16(pageData, 41);
            this.VariableColums = BitConverter.ToUInt16(pageData, 43);
            this.ColumnCount = BitConverter.ToUInt16(pageData, 45);
            this.IndexCount = BitConverter.ToUInt32(pageData, 47);
            this.RealIndexCount = BitConverter.ToUInt32(pageData, 51);
            this.RowPageMap = BitConverter.ToUInt32(pageData, 55);
            this.FreeSpacePageMap = BitConverter.ToUInt32(pageData, 59);

            for(uint i=0;i<this.RealIndexCount;i++)
            {

            }

            for (uint i = 0; i < this.ColumnCount; i++)
            {

            }
        }
    }
}
