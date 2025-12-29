using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Employee.API.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _config;

        public DapperContext(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}
