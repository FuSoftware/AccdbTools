using AccdbTools.ACCDB.Generic;
using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Jet4
{
    class Jet4File : JetFile
    {
        public Jet4File(byte[] data) : base(data)
        {

        }

        public override Header LoadHeader(byte[] data)
        {
            return new Jet4Header(data);
        }

        public override Page LoadPage(byte[] data, int index)
        {
            ushort type = Page.DataPageType(data, index, PageLength);

            switch (type)
            {
                case (ushort)PageType.TableDefinition:
                    return new Pages.Jet4TableDefinitionPage(data.Skip(index * PageLength).Take(PageLength).ToArray());
                case (ushort)PageType.Data:
                    return new Pages.Jet4DataPage(data.Skip(index * PageLength).Take(PageLength).ToArray());
                default:
                    return new PlaceHolderPage(type);
            }
        }
    }
}
