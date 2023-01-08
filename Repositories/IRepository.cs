using MySql.Data.MySqlClient;
using ServerAPI.Models;

namespace ServerAPI.Repositories
{
    public interface IRepository
    {
       MySqlConnection Connection();
         
    }
}
