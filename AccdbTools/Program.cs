using AccdbTools.ACCDB;
using AccdbTools.ACCDB.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccdbTools.Database;

namespace AccdbTools
{
    class Program
    {
        public static void Main()
        {
            JetFile f = JetFileLoader.LoadFile(@"D:\LLIC3_AMRA.accdb");
            Database.Database d = new Database.Database(f);
            d.Load();
            Console.ReadLine();
        }
    }
}
