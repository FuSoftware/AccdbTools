﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic.Pages
{
    enum ColumnType
    {
        Boolean = 1,
        Int8 = 2,
        Int16 = 3,
        Int32 = 4,
        Currency = 5,
        Single = 6,
        Double = 7,
        DateTime = 8,
        Binary = 9,
        Text = 10,
        OLE = 11,
        Memo = 12,
        GUID = 15,
        FixedPoint = 16,
        Complex = 18
    }

    enum ColumnFlags
    {
        FixedLength = 0x01,
        Nullable = 0x02,
        Autonumber = 0x04,
        uA = 0x08,
        Hidden = 0x10,
        uB = 0x20,
        AutoGUID = 0x40,
        Hyperlink = 0x80,
        CompressedUnicode = 0x0100,
        ModernPackage = 0x1000,
        uC = 0x4000,
        uD = 0x8000
    }

    enum IndexFlags
    {
        Unique = 0x01,
        IgnoreNulls = 0x02,
        Required = 0x08
    }

    class Index
    {
        
    }

    class RealIndex
    {
        public struct ColData
        {
            public uint ColumnId { get; set; }
            public byte Flags { get; set; }

            public ColData(uint ColumnId, byte Flags)
            {
                this.Flags = Flags;
                this.ColumnId = ColumnId;
            }
        }
        public List<ColData> Columns { get; set; }
        public uint IndexRowCount { get; set; }
        public uint FirstIndexPage { get; set; }
        public uint Flags { get; set; }
        public uint B2 { get; set; }
    }

    class Column
    {
        ColumnType Type;
        ushort ColumnId;
        ushort VariableColumnNumber;
        ushort ColumnFlags;
        ushort FixedOffset;
        ushort Length;
        string Name;
    }

    class TableDefinitionPage : Page
    {
        public ushort PageSignature { get; set; }
        public uint NextPage { get; set; }
        public ulong Length { get; set; }
        public ulong Rows { get; set; }
        public ulong Autonumber { get; set; }
        public byte TableFlag { get; set; }

        public ushort NextColumnId { get; set; }
        public ushort VariableColums { get; set; }
        public ushort ColumnCount { get; set; }
        public ulong IndexCount { get; set; }
        public ulong RealIndexCount { get; set; }
        public ulong RowPageMap { get; set; }
        public ulong FreeSpacePageMap { get; set; }

        public TableDefinitionPage(byte[] pageData)
        {
            this.Load(pageData);
        }

        void Load(byte[] pageData)
        {

        }
    }
}