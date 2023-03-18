using Utility.Base.Abstration;
using Utility.Base.Attributes;
using Utility.Base.Attributes.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Utility.SQL_Server
{
    public abstract class Model
    {
        public int ID { get; set; }

        [HideProperty]
        public string GenerateInsert => $"VALUES({string.Join(",", PropertyManager<Model>.PropertyNames)})";

        internal void AddValues(SqlCommand command)
        {
            var g = PropertyManager<Model>.PropertyNames;

            GetType().GetProperties().Where(prop => prop.Name[0] != '_').Select(e => e).ToList().ForEach(prop => {command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(prop));});
        }
    }
}


