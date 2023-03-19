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
        public static TableName NewTable(string name) { return new TableName(name); }
        
        [HideProperty]
        public int Lenght => PropertyManager.PropertyCount(this);
        [HideProperty]
        public List<string> AllElements => PropertyManager.PropertyValues<TableName>(this).Select(e => (string) e).ToList();
        
    }
}