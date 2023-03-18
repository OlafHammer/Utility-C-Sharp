using Utility.Base.Attributes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.SQL_Server;

namespace Database_Reference.SQL_Server
{
    public abstract class TableNames
    {
        [HideProperty]
        public int Lenght => GetType().GetProperties().Where(prop => prop.Name[0] != '_').Select(e => e.Name).ToList().Count;
        [HideProperty]
        public List<string> AllElements => GetType().GetProperties().Where(prop => prop.Name[0] != '_').Select(e => e.Name).ToList();
    }
}
