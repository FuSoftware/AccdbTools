using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic
{
    abstract class Header
    {
        public static byte[] RC4Key { get => new byte[4]{ 0xc7, 0xda, 0x39, 0x6b}; }
        public ushort SystemCollation { get; set; }
        public ushort SystemCodePage { get; set; }
        public uint DatabaseKey { get; set; }
        public string DatabasePassword { get; set; }
        public double CreationDate { get; set; }
    }
}
