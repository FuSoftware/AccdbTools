using AccdbTools.ACCDB.Generic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccdbTools.Database
{
    class Table
    {
        List<TableDefinitionPage> DefinitionPages { get; set; }
        List<DataPage> DataPages { get; set; }
    }
}
