using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utility.SQL_Server
{
    public class TableName : IComparable<TableName?>
    {

        public static implicit operator string(TableName d) => d.Name;

        private string Name { get; }

        internal TableName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(TableName? obj)
        {
            if (obj == null) return -1;
            if (obj.Name == Name) return 0;
            return 1;
        }
    }
}
