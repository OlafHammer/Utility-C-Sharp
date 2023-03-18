using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace Utility.SQL_Server
{
    public class MainDatabase
    {

        private string ServerConnectionString { get; }

        public MainDatabase(string serverConnectionString)
        {
            ServerConnectionString = serverConnectionString;
        }

        public void Insert(Model model, TableName name)
        {
            string sqlStatement = $"INSERT INTO {name} {model.GenerateInsert}";

            using (SqlConnection connection = new (ServerConnectionString))
            {
                SqlCommand command = new (sqlStatement, connection);

                model.AddValues(command);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }
        }

    }
}