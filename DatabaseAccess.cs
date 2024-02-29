using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Apptest
{
    public static class DatabaseAccess
    {
        private static string connectionString = "Server=localhost; Database=BaselCoinDB; Trusted_Connection=True;";

        public static bool ValidateUser(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT PasswordHash FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var storedPasswordHash = reader.GetString(0);
                    return VerifyPassword(password, storedPasswordHash);
                }
                return false;
            }
        }

        private static bool VerifyPassword(string password, string storedPasswordHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
                return hashedPassword == storedPasswordHash;
            }
        }
    }

}
