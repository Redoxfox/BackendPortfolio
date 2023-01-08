using MySql.Data.MySqlClient;


namespace ServerAPI.Repositories
{
    /*public class RepositoryMysql: IDisposable
    {
        public MySqlConnection Connection;

        public RepositoryMysql(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }

    }*/

   public class RepositoryMysql
   {
       
       private readonly IConfiguration _configuration;
       public  RepositoryMysql(IConfiguration configuration)
       {
            _configuration = configuration;
       }

        public MySqlConnection Connection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new MySqlConnection(connectionString);
        }

    }
}
