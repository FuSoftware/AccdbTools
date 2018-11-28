using AccdbTools.ACCDB.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AccdbTools.ACCDB.Jet3;
using AccdbTools.ACCDB.Jet4;

namespace AccdbTools.ACCDB
{
    class AccdbFile
    {
        FileFormat Format { get; set; }
        Header Header { get; set; }
        List<Page> Pages { get; set; }
        int PageLength { get => 4096; }

        public AccdbFile(string file)
        {
            this.Load(file);
        }

        public AccdbFile(byte[] data)
        {
            this.Load(data);
        }

        public void Load(string file)
        {
            this.Load(File.ReadAllBytes(file));
        }

        public void Load(byte[] data)
        {
            var l = Page.DataPageTypeList(data);

            this.Format = new FileFormat(data);

            if (Format.IsJet3)
            {
                this.Header = new Jet3Header(data);
            }
            else
            {
                this.Header = new Jet4Header(data);
            }

            LoadPages(data);
        }

        public void LoadPages(byte[] data)
        {
            this.Pages = new List<Page>();
            for(int i = 1; i < data.Length / 4096; i++)
            {
                this.Pages.Add(LoadPage(data, i));
            }
        }

        public Page LoadPage(byte[] data, int index)
        {
            uint type = Page.DataPageType(data, index, PageLength);

            switch(type)
            {
                case (uint)PageType.TableDefinition:
                    return new Jet4.Pages.Jet4TableDefinitionPage(data.Skip(index * PageLength).Take(PageLength).ToArray());
                default:
                    return null;
            }
        }
    }
}
