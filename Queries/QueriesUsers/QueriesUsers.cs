using MySql.Data.MySqlClient;
using ServerAPI.Helpers;
using ServerAPI.Interfaces;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesUsers;
using ServerAPI.Repositories;

namespace ServerAPI.Queries.QueriesUsers
{
    public class QueriesUsers : IQueriesUser
    {
        private readonly IConfiguration _configuration;
        //private readonly RepositoryMysql _repository;

        public QueriesUsers(IConfiguration configuration)
        {
            _configuration = configuration;
            //_repository = repository;
        }
        public async Task<EntityRequest> InsertUserDb(EntityUser entityUser)
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            string query = "insert into proyecto.users(id,nick,name,email,password,rol) values(null,'"
                           + entityUser.nick + "','" + entityUser.name + "','" + entityUser.email + "','" +
                           entityUser.password + "','" + entityUser.rol + "');";

            MySqlDataReader myReader;
            using (Connection)
            {
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    myReader = mycommand.ExecuteReader();
                    while (await myReader.ReadAsync())
                    {

                    }
                    myReader.Close();
                }
            }
          
            var resultQuery = new EntityRequest()
            {
                request=true,
                msg =entityUser.nick,
            };

            return resultQuery;
        }

        public async Task<EntityUser> SelectUserDb(EntityLogin entityLogin)
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            var User = new EntityUser();
            string query = @"select id, nick, name, email, password, rol from proyecto.users where email='" + entityLogin.email + "';";

            MySqlDataReader myReader;
            using (Connection)
            {
                //await connection.OpenAsync();
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    myReader = mycommand.ExecuteReader();
                    //table.Load(myReader);
                    while (await myReader.ReadAsync())
                    {
                        var newUser = new EntityUser()
                        {
                            id = myReader.GetInt32(0),
                            nick = myReader.GetString(1),
                            name = myReader.GetString(2),
                            email = myReader.GetString(3),
                            password = myReader.GetString(4),
                            rol = myReader.GetString(5)
                        };

                        User = newUser;

                    }

                    myReader.Close();
                    //connection.Close();
                }
            }

            return User;
        }
    }
}
