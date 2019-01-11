using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic
{
    abstract class JetFile
    {
        public FileFormat Format { get; set; }
        public Header Header { get; set; }
        public Page[] Pages { get; set; }
        public int PageLength { get => 4096; }

        public JetFile()
        {

        }

        public JetFile(byte[] data)
        {
            this.Load(data);
        }

        public void Load(byte[] data)
        {
            this.Header = LoadHeader(data);
            this.Pages = LoadPages(data);
        }

        public Page[] LoadPages(byte[] data)
        {
            int PageCount = data.Length / this.PageLength;
            Page[] pages = new Page[PageCount];

            Console.WriteLine("Loading {0} pages", PageCount);

            Stopwatch sw = new Stopwatch();
            for (int i = 1; i < PageCount; i++)
            {
                sw.Restart();
                pages[i] = LoadPage(data, i);
                sw.Stop();
                Console.WriteLine("Loaded Page {1} in {0}ms", sw.ElapsedMilliseconds, i);
            }
            return pages;
        }

        public abstract Header LoadHeader(byte[] data);
        public abstract Page LoadPage(byte[] data, int index);
    }
}
