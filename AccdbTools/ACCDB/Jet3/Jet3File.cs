using AccdbTools.ACCDB.Generic;
using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet3
{
    class Jet3File : JetFile
    {
        public Jet3File(byte[] data) : base(data)
        {

        }

        public override Header LoadHeader(byte[] data)
        {
            return new Jet3Header(data);
        }

        public override Page LoadPage(byte[] data, int index)
        {
            ushort type = Page.DataPageType(data, index, PageLength);

            switch (type)
            {
                case (ushort)PageType.TableDefinition:
                    //return new Pages.Jet3TableDefinitionPage(data.Skip(index * PageLength).Take(PageLength).ToArray());
                case (ushort)PageType.Data:
                    //return new Pages.Jet3DataPage(data.Skip(index * PageLength).Take(PageLength).ToArray());
                default:
                    return new PlaceHolderPage(type);
            }
        }
    }
}
