using System;
using System.Collections.Generic;
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
        public List<Page> Pages { get; set; }
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

        public List<Page> LoadPages(byte[] data)
        {
            List<Page> pages = new List<Page>();
            for (int i = 1; i < data.Length / this.PageLength; i++)
            {
                pages.Add(LoadPage(data, i));
            }
            return pages;
        }

        public abstract Header LoadHeader(byte[] data);
        public abstract Page LoadPage(byte[] data, int index);
    }
}
