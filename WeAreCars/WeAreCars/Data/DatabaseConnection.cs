using System;
using System.Data.SqlClient;

namespace WeAreCars.Data
{
    /// <summary>
    /// A utility helper class acting as the core entry-point for database communication setups.
    /// </summary>
    public static class DatabaseConnection
    {
        // Using Windows Authentication (Integrated Security=True) for WeAreCars database
        private static string connectionString = @"Server=localhost\SQLEXPRESS;Database=WeAreCars;Integrated Security=True;";

        /// <summary>
        /// Instantiates and provisions a new SQL Connection to interface with the database.
        /// </summary>
        /// <returns>An initialized SqlConnection instance</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
