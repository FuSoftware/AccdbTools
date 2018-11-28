using AccdbTools.ACCDB;
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
            AccdbFile f = new AccdbFile(@"D:\A.accdb");
            Console.ReadLine();
        }
    }
}
