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
using System.Reflection.Metadata;
using System.Net.NetworkInformation;

namespace Utility.SQL_Server
{
    public abstract class Model
    {
        [HideProperty]
        private const AvalibleAttributes hideProp = AvalibleAttributes.HideProperty;
        [HideProperty]
        private const AvalibleAttributes sqlAutoIncr = AvalibleAttributes.SQLAutoIncrementingKey;

        [SQLAutoIncrementingKey]
        public int ID { get; set; }

        [HideProperty]
        public string GenerateInsert => $" ({string.Join(",", PropertyManager.PropertyNames(this, hideProp))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this, hideProp).Select(e => "@" + e))})";

        [HideProperty]
        public string GenerateInsert_AutoId => $" ({string.Join(",", PropertyManager.PropertyNames(this, hideProp, sqlAutoIncr))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this, hideProp, sqlAutoIncr).Select(e => "@" + e))})";

        public void AddValues(SqlCommand command)
        {
            PropertyManager.Propertys(this, hideProp).Where(item => command.Parameters.Contains(item)).ToList().ForEach(prop => command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(this)));
        }
    }
}


