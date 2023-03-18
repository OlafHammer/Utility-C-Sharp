using Utility.Base.Attributes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.SQL_Server;
using Utility.Base.Abstration;

namespace Utility.SQL_Server
{
    public abstract class TableNames
    {
        [HideProperty]
        public int Lenght => PropertyManager<TableNames>.PropertyCount;
        [HideProperty]
        public List<string> AllElements => PropertyManager<TableNames>.PropertyNames;
    }
}