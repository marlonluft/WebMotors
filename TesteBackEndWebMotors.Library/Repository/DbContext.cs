using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TesteBackEndWebMotors.Library.Repository
{
    public class DbContext : IDbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection");
        }

        public SqlConnection Get()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}
