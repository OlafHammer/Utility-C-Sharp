using Microsoft.VisualBasic;
using System.Data.SqlClient;
using Utility.Base.ResultHandler;

namespace Utility.SQL_Server
{
    public class MainDatabase
    {

        private string ServerConnectionString { get; }

        public MainDatabase(string serverConnectionString)
        {
            ServerConnectionString = serverConnectionString;
        }

        public SQLResult<int> Insert(Model model, TableName name) 
        {
            string sqlStatement = $"INSERT INTO {name} {model.GenerateInsert}";

            using SqlConnection connection = new(ServerConnectionString);
            SqlCommand command = new(sqlStatement, connection);

            model.AddValues(command);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                return new SQLResult<int> { Output = result, Statement = sqlStatement, Command = command };
            }
            catch (Exception ex)
            {
                return new SQLResult<int> { Error = ex, Output = 0,  Statement = sqlStatement, Command = command };
            }
        }

    }
}