using System.Data.SqlClient;
using Utility.Base.Abstraction;
using Utility.Base.Attributes;
using Utility.Base.Attributes.Classes;

namespace Utility.SQL_Server;

/// <summary>
///     Provides an abstract Class for SQL Class Models with unique identifier
///     Also provides SQL Statement generation as well as injection save Parameter insertion
/// </summary>
public abstract class Model
{
    [HideProperty] private const AvailableAttributes HideProp = AvailableAttributes.HideProperty;

    [HideProperty] private const AvailableAttributes SqlAutoIncr = AvailableAttributes.SqlAutoIncrementingKey;

    /// <summary>
    ///     SQL Primary key / Unique identifier
    /// </summary>
    [SqlAutoIncrementingKey]
    public int Id { get; set; }

    /// <summary>
    ///     Returns a String in the Format " (value1, value2) VALUES(@value1, @value2)" containing all Properties not marked as
    ///     HideProperty as values
    /// </summary>
    [HideProperty]
    public string GenerateInsert =>
        $" ({string.Join(",", PropertyManager.PropertyNames(this, HideProp))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this, HideProp).Select(e => "@" + e))})";

    /// <summary>
    ///     Returns a String in the Format " (value1, value2) VALUES(@value1, @value2)" containing all Properties not marked as
    ///     HideProperty or SqlAutoIncrement as values
    /// </summary>
    [HideProperty]
    public string GenerateInsertAutoId =>
        $" ({string.Join(",", PropertyManager.PropertyNames(this, HideProp, SqlAutoIncr))}) VALUES({string.Join(",", PropertyManager.PropertyNames(this, HideProp, SqlAutoIncr).Select(e => "@" + e))})";

    /// <summary>
    ///     Adds Parameters with Value by
    ///     taking all available Properties which aren't marked with the "HideProperty" Attribute
    ///     then filtering all Properties out which aren't within the SQL Statement (to filter primary keys out)
    ///     then adding parameter values by the equivalent entity within the SQL Statement
    /// </summary>
    /// <param name="command">SQL Command with valid Statement where variables are marked with @{entity}</param>
    public void AddValues(SqlCommand command)
    {
        PropertyManager.Properties(this, HideProp).Where(item => command.CommandText.Contains(item.Name)).ToList()
            .ForEach(prop => command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(this)));
    }
}