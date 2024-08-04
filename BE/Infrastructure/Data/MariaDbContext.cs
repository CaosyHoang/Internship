using Contracts;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Infrastructure.Data
{
    public class MariaDbContext : IDapperContext
    {
        public IDbConnection Connection { get; }

        public MariaDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("mySqlConnection"));
        }
    }
}
