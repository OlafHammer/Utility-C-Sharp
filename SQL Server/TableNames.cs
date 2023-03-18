using Utility.Base.Attributes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.SQL_Server;
using Utility.Base.Abstration;

namespace Database_Reference.SQL_Server
{
    public abstract class TableNames
    {
        [HideProperty]
        public int Lenght => PropertyManager<TableNames>.Propertys.Select(e => e.Name).ToList().Count;
        [HideProperty]
        public List<string> AllElements => PropertyManager<TableNames>.Propertys.Select(e => e.Name).ToList();
    }
}
