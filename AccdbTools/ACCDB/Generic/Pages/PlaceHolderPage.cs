using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.ACCDB.Generic.Pages
{
    class PlaceHolderPage : Page
    {
        public PlaceHolderPage(ushort signature)
        {
            this.PageSignature = (PageType)signature;
        }
    }
}
