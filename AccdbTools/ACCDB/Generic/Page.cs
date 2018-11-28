﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic
{
    enum PageType
    {
        Header = 0x0100,
        Data = 0x0101,
        TableDefinition = 0x0102,
        Unknown1 = 0x0103,
        Unknown2 = 0x0104,
    }

    class Page
    {
        public Page()
        {

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
