using System;
using System.Data.SqlClient;

namespace Apptest
{
    public static class Logger
    {
        private static string connectionString = "Server=localhost; Database=BaselCoinDB; Trusted_Connection=True;";

        public static void Log(string username, string action)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO UserLogs (Username, Action) VALUES (@Username, @Action)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Action", action);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
