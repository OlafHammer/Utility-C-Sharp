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

    /// <summary>
    /// Provides an abstrace Class for SQL Class Models with unique identifier
    /// Also provides SQL Statement generation as well as injection save Parameter insertion
    /// </summary>
    public abstract class Model
    {
        [HideProperty]
        private const AvalibleAttributes hideProp = AvalibleAttributes.HideProperty;
        [HideProperty]
        private const AvalibleAttributes sqlAutoIncr = AvalibleAttributes.SQLAutoIncrementingKey;

        /// <summary>
        /// SQL Primarykey / Unique identifier
        /// </summary>
        [SQLAutoIncrementingKey]
        public int ID { get; set; }

        /// <summary>
        /// Returns a String in the Format " (value1, value2) VALUES(@value1, @value2)" containing all propertys not marked as HideProperty as values
        /// </summary>
        [HideProperty]
        public string GenerateInsert => $" ({string.Join(",", PropertyManager.PropertyNames(this, hideProp))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this, hideProp).Select(e => "@" + e))})";

        /// <summary>
        /// Returns a String in the Format " (value1, value2) VALUES(@value1, @value2)" containing all propertys not marked as HideProperty or SqlAutoIncrement as values
        /// </summary>
        [HideProperty]
        public string GenerateInsert_AutoId => $" ({string.Join(",", PropertyManager.PropertyNames(this, hideProp, sqlAutoIncr))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this, hideProp, sqlAutoIncr).Select(e => "@" + e))})";

        /// <summary>
        /// Adds Parameters with Value by 
        /// taking all available Propertys which arent marked with the "HideProperty" Attribute
        /// then filtering all Propertys out which arent within the SQL Statement (to filter primary keys out)
        /// then adding parameter values by the equivilent entity within the SQL Statement
        /// </summary>
        /// <param name="command">SQL Command with valid Statment where variables are marked with @{entity}</param>
        public void AddValues(SqlCommand command)
        {
            PropertyManager.Propertys(this, hideProp).Where(item => command.CommandText.Contains(item.Name)).ToList().ForEach(prop => command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(this)));
        }
    }
}


