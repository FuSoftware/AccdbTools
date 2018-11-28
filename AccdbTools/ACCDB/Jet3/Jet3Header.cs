using AccdbTools.ACCDB.Generic;
using AccdbTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet3
{
    class Jet3Header : Header
    {
        public Jet3Header(byte[] data)
        {
            byte[] headerData = RC4.Decrypt(RC4Key, data.Skip(24).Take(126).ToArray());

            this.SystemCollation = BitConverter.ToUInt16(headerData, 0x22);
            this.SystemCodePage = BitConverter.ToUInt16(headerData, 0x24);
            this.CreationDate = BitConverter.ToDouble(headerData, 0x5A);
            this.DatabaseKey = BitConverter.ToUInt32(headerData, 0x26);
            this.DatabasePassword = BitConverter.ToString(headerData, 0x2A, 20);
        }
    }
}
