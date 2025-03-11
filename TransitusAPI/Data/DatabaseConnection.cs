using System.Data;
using System.Data.SqlClient;

namespace TransitusAPI.Data
{
    /// <summary>
    /// Provides database connection functionality for the Transitus application
    /// </summary>
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the DatabaseConnection class
        /// </summary>
        /// <param name="configuration">The application configuration containing connection strings</param>
        public DatabaseConnection(IConfiguration configuration)
        {
            // Get connection string from configuration
            _connectionString = configuration.GetConnectionString("TransitusDatabase") 
                ?? throw new InvalidOperationException("Connection string 'TransitusDatabase' not found.");
        }

        /// <summary>
        /// Creates and returns a new SQL connection
        /// </summary>
        /// <returns>An open SQL connection</returns>
        public IDbConnection CreateConnection()
        {
            // Create a new SQL connection using the connection string
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
} 