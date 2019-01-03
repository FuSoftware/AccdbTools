using AccdbTools.ACCDB.Jet3;
using AccdbTools.ACCDB.Jet4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic
{
    class JetFileLoader
    {
        public static JetFile LoadFile(string path)
        {
            return JetFileLoader.LoadFile(File.ReadAllBytes(path));
        }
    
        public static JetFile LoadFile(byte[] data)
        {
            JetFile f = null;

            //Retrieve Main Header
            FileFormat Format = new FileFormat(data);

            //Load Jet4
            if (Format.IsJet3)
            {
                //Load Jet3
                f = new Jet3File(data);
            }
            else
            {
                //Load Jet4
                f = new Jet4File(data);
                f.Format = Format;
            }

            return f;
        }
    }
}
