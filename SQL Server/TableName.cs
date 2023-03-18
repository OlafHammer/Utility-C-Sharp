using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SQL_Server
{
    public class TableName
    {
        private string Name { get; }

        internal TableName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
