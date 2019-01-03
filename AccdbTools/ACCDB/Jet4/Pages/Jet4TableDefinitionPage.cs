using AccdbTools.ACCDB.Generic;
using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet4.Pages
{
    class Jet4Index : Index
    {
        public uint C1 { get; set; }
        public uint C6 { get; set; }

        public Jet4Index(byte[] data)
        {
            this.Load(data);
        }

        void Load(byte[] data)
        {
            this.C1 = BitConverter.ToUInt32(data, 0);
            this.IndexNumber = BitConverter.ToUInt32(data, 4);
            this.IndexColumnNumber = BitConverter.ToUInt32(data, 8);
            this.C2 = data[12];
            this.C3 = BitConverter.ToUInt32(data, 13);
            this.C4 = BitConverter.ToUInt32(data, 17);
            this.C5 = BitConverter.ToUInt16(data, 21);
            this.IndexType = data[23];
            this.C6 = BitConverter.ToUInt32(data, 24);
        }
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

        public Jet4Column(byte[] data)
        {
            this.Load(data);
        }

        void Load(byte[] data)
        {
            this.Type = (ColumnType)data[0];
            this.uA = BitConverter.ToUInt32(data, 1);
            this.ColumnId = BitConverter.ToUInt16(data, 5);
            this.VariableColumnNumber = BitConverter.ToUInt16(data, 7);
            this.ColumnIndex = BitConverter.ToUInt16(data, 9);

            //Various (4 bytes)
            if(this.Type == ColumnType.Text)
            {
                //Text

            }
            else if(this.Type == ColumnType.Int8 || this.Type == ColumnType.Int16 || this.Type == ColumnType.Int32)
            {
                //Decimal
            }
            else if(this.Type == ColumnType.OLE || this.Type == ColumnType.Memo)
            {
                //Complex
            }
            else
            {

            }

            this.ColumnFlags = BitConverter.ToUInt16(data, 15);
            this.uB = BitConverter.ToUInt32(data, 17);
            this.FixedOffset = BitConverter.ToUInt16(data, 21);
            this.Length = BitConverter.ToUInt16(data, 23);
        }
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
            this.PageSignature = (PageType)BitConverter.ToUInt16(pageData, 0);
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

            List<Jet4RealIndex> RealIndexes = new List<Jet4RealIndex>();
            List<Jet4Index> Indexes = new List<Jet4Index>();

            this.Columns = new List<Column>();

            int offset = 59 + 4;
            for (int i=0;i<(long)this.RealIndexCount;i++)
            {
                RealIndexes.Add(new Jet4RealIndex(pageData.Skip(offset).Take(12).ToArray()));
                offset += 12;
            }

            for (int i = 0; i < (long)this.ColumnCount; i++)
            {
                Columns.Add(new Jet4Column(pageData.Skip(offset).Take(25).ToArray()));
                offset += 25;
            }

            for (int i = 0; i < (long)this.ColumnCount; i++)
            {
                ushort j = BitConverter.ToUInt16(pageData, offset);
                offset += 2;
                Columns[i].Length = j;
                Columns[i].Name = Encoding.Unicode.GetString(pageData.Skip(offset).Take(j).ToArray());
                offset += j;
            }

            for (int i = 0; i < (long)this.RealIndexCount; i++)
            {
                RealIndexes[i].LoadSecondPart(pageData.Skip(offset).Take(52).ToArray());
                offset += 52;
            }

            //Every Index
            for (int i = 0; i < (long)this.IndexCount; i++)
            {
                Indexes.Add(new Jet4Index(pageData.Skip(offset).Take(28).ToArray()));
                offset += 28;
            }

            for (int i = 0; i < (long)this.IndexCount; i++)
            {
                ushort j = BitConverter.ToUInt16(pageData, offset);
                offset += 2;
                Indexes[i].Name = Encoding.Unicode.GetString(pageData.Skip(offset).Take(j).ToArray());
                offset += j;
            }

            //Until ColumnID 0XFFFF
            uint id = BitConverter.ToUInt16(pageData, offset);
            offset += 2;
            while (id != 0xFFFF)
            {
                Column col = this.Columns.Single(c => c.ColumnId == id);
                col.Args.Add(BitConverter.ToUInt32(pageData, offset));
                col.Args.Add(BitConverter.ToUInt32(pageData, offset + 4));
                offset += 8;
                
                id = BitConverter.ToUInt16(pageData, offset);
                offset += 2;
            }
        }
    }
}
