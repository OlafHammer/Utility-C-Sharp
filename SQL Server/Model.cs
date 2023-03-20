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
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

namespace Utility.SQL_Server
{
    public abstract class Model
    {
        public int ID { get; set; }

        [HideProperty]
        public string GenerateInsert => $" ({string.Join(",", PropertyManager.PropertyNames(this))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this).Select(e => "@" + e))})";

        public void AddValues(SqlCommand command)
        {
            PropertyManager.Propertys(this).ForEach(prop => command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(this)));
        }
    }
}


