using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic
{
    class FileFormat
    {
        public uint JetVersion { get; set; }
        public string Format { get; set; }
        public uint MagicNumber { get; set; }

        public bool IsJet3
        {
            get => JetVersion == 0;
        }

        public FileFormat(byte[] data)
        {
            Console.WriteLine(BitConverter.IsLittleEndian);
            this.MagicNumber = BitConverter.ToUInt32(data, 0x0);
            this.Format = Encoding.Default.GetString(data.Skip(0x04).Take(16).ToArray());
            this.JetVersion = BitConverter.ToUInt32(data, 0x14);
        }
    }
}
