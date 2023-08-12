using System.Data.SqlClient;
using Utility.Base.ResultHandler;

namespace Utility.SQL_Server;

public class DatabaseProvider
{
    public DatabaseProvider(string serverConnectionString)
    {
        ServerConnectionString = serverConnectionString;
    }

    public DatabaseProvider(string serverConnectionString, TableName tableName)
    {
        ServerConnectionString = serverConnectionString;
        TableName = tableName;
    }


    private string ServerConnectionString { get; }
    private TableName? TableName { get; }

    /// <summary>
    ///     Inserts a Model object into a given SQL table.
    /// </summary>
    /// <param name="model">The model to insert</param>
    /// <param name="name">SQL Table Name</param>
    /// <param name="useAutoKey">SQL Table uses auto incrementing primary Key</param>
    /// <returns>A SQL Result object containing the amount of changed lines as Output and the SQL command object</returns>
    /// <returns>If successful: Output > 0 and Success is true</returns>
    /// <returns>If successful: Output = 0 and Success is false; Also contains error statement</returns>
    public SqlResult<int> Insert(Model model, TableName name, bool useAutoKey = true)
    {
        var sqlStatement = useAutoKey
            ? $"INSERT INTO {name} {model.GenerateInsertAutoId}"
            : $"INSERT INTO {name} {model.GenerateInsert}";

        return RunSQlStatement(sqlStatement, model);
    }

    public SqlResult<int> Insert(Model model, bool useAutoKey = true)
    {
        return Insert(model, TableName ?? throw new NullReferenceException("TableName is null"), useAutoKey);
    }

    public SqlResult<int> UpdateByNullableModel(Model model, TableName name)
    {
        var sqlStatement = $"UPDATE {name} SET {model.GenerateUpdateByNullable}";

        return RunSQlStatement(sqlStatement, model);
    }

    public SqlResult<int> UpdateByNullableModel(Model model)
    {
        return UpdateByNullableModel(model, TableName ?? throw new NullReferenceException("TableName is null"));
    }

    public SqlResult<int> Update(Model oldModel, Model model, TableName name)
    {
        var sqlStatement = $"UPDATE {name} SET {model.GenerateUpdateByComparison(oldModel)}";

        return RunSQlStatement(sqlStatement, model);
    }

    public SqlResult<int> Update(Model oldModel, Model model)
    {
        return Update(oldModel, model, TableName ?? throw new NullReferenceException("TableName is null"));
    }


    public SqlResult<int> Update(Model model, TableName name)
    {
        // TODO Load old Model from db and check which properties have changed

        Model sqlModel = model; // TODO Load from DB

        var sqlStatement = $"UPDATE {name} SET {model.GenerateUpdateByComparison(sqlModel)}";

        return RunSQlStatement(sqlStatement, model);
    }

    public SqlResult<int> Update(Model model)
    {
        return Update(model, TableName ?? throw new NullReferenceException("TableName is null"));
    }

    private SqlResult<int> RunSQlStatement(string sqlStatement, Model model)
    {
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