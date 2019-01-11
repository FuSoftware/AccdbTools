using AccdbTools.ACCDB.Generic;
using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.Database
{
    class Database
    {
        public JetFile File { get; set; }
        public List<Table> Tables { get; set; }

        public Database(JetFile file)
        {
            this.File = file;
        }

        public void Load()
        {
            Page[] pages = new Page[this.File.Pages.Length];
            this.File.Pages.CopyTo(pages,0);
            Dictionary<Page, Table> tables = new Dictionary<Page, Table>();

            List<Page> Done = new List<Page>();
            List<List<TableDefinitionPage>> TableDefinitions = new List<List<TableDefinitionPage>>();

            foreach(Page p in pages.Where(p=>p.PageSignature == PageType.TableDefinition))
            {
                if (!Done.Contains(p))
                {
                    TableDefinitionPage tdp = (TableDefinitionPage)p;
                    TableDefinitions.Add(new List<TableDefinitionPage>());
                    uint n = tdp.NextPage;

                    do
                    {
                        TableDefinitions.Last().Add(tdp);
                        Done.Add(tdp);
                        n = tdp.NextPage;
                    } while (n > 0);
                }
            }

            Console.WriteLine(TableDefinitions.Count);
        }
    }
}
