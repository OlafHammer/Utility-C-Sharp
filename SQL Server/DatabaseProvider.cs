using System.Data.SqlClient;
using Utility.Base.ResultHandler;

namespace Utility.SQL_Server;

public class DatabaseProvider
{
    public DatabaseProvider(string serverConnectionString)
    {
        ServerConnectionString = serverConnectionString;
    }

    private string ServerConnectionString { get; }

    /// <summary>
    ///     Inserts a Model object into a given SQL table.
    /// </summary>
    /// <param name="model">The model to insert</param>
    /// <param name="name">SQL Table Name</param>
    /// <param name="useAutoKey">SQL Table uses auto incrementing primary Key</param>
    /// <returns>A SQL Result object containing the amount of changed lines as Output and the SQL command object</returns>
    /// <returns>If successful: Output > 0 and Success is true</returns>
    /// <returns>If successful: Output = 0 and Success is false; Also contains error statement</returns>
    public SqlResult<int> Insert(Model model, TableName name, bool useAutoKey)
    {
        var sqlStatement = useAutoKey ? $"INSERT INTO {name} {model.GenerateInsertAutoId}" : $"INSERT INTO {name} {model.GenerateInsert}";

        using SqlConnection connection = new(ServerConnectionString);

        SqlCommand command = new(sqlStatement, connection);
        model.AddValues(command);

        try
        {
            connection.Open();
            var result = command.ExecuteNonQuery();
            return new SqlResult<int> { Output = result, Command = command };
        }
        catch (Exception ex)
        {
            return new SqlResult<int> { Error = ex, Output = 0, Command = command };
        }
    }
}