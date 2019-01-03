using AccdbTools.ACCDB;
using AccdbTools.ACCDB.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools
{
    class Program
    {
        public static void Main()
        {
            JetFile f = JetFileLoader.LoadFile(@"D:\Database1.accdb");
            Console.ReadLine();
        }
    }
}
