using MySql.Data.MySqlClient;

namespace ServerAPI.Data
{
    public class DbConnection
    {
        private readonly IConfiguration _configuration;

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<MySqlConnection> GetConnection()
        {
            
            string sqlDataSource = _configuration.GetConnectionString("DefaulfConnection");
            
            using (MySqlConnection connection = new MySqlConnection(sqlDataSource))
            {
                await connection.OpenAsync();
                return connection;
            }

        }
    }
}
