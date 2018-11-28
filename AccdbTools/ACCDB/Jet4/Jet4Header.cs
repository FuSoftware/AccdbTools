using AccdbTools.ACCDB.Generic;
using AccdbTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet4
{
    class Jet4Header : Header
    {
        public Jet4Header(byte[] data)
        {
            byte[] headerData = RC4.Decrypt(RC4Key, data.Skip(24).Take(128).ToArray());

            this.SystemCollation = BitConverter.ToUInt16(headerData, 0x56);
            this.SystemCodePage = BitConverter.ToUInt16(headerData, 0x24);
            this.CreationDate = BitConverter.ToDouble(headerData, 0x5A);
            this.DatabaseKey = BitConverter.ToUInt32(headerData, 0x26);
            this.DatabasePassword = Encoding.Default.GetString(headerData.Skip(0x2A).Take(40).ToArray());
        }
    }
}
