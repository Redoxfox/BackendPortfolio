using MySql.Data.MySqlClient;
using ServerAPI.Helpers;
using ServerAPI.Interfaces.InterfacesBets;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesBets;
using ServerAPI.Repositories;

namespace ServerAPI.Queries.QueriesBets
{
    public class QueriesBets : IQueriesBets
    {
        //private readonly RepositoryMysql _repository;

        //RepositoryMysql repository
        private readonly IConfiguration _configuration;

        public QueriesBets(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<EntityMatch>> SelectMatches()
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            var ListMatches = new List<EntityMatch>();
            string query = @"select id, nro_fecha, id_liga, equipo_1, gol_eq1, equipo_2, gol_eq2, 
            local_eq1, local_eq2, estado, temporada,  fecha  from proyecto.calendario;";
   
            MySqlDataReader myReader;
            //_repository.Connection
            using (Connection)
            {
                await Connection.OpenAsync();
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    myReader = mycommand.ExecuteReader();
                    //table.Load(myReader);
                    while (await myReader.ReadAsync())
                    {
                        var newPartido = new EntityMatch();
                        newPartido.id = myReader.GetInt32(0);
                        newPartido.nro_fecha = myReader.GetInt32(1);
                        newPartido.id_liga = myReader.GetInt32(2);
                        newPartido.equipo_1 = myReader.GetInt32(3);
                        newPartido.gol_eq1 = myReader.GetInt32(4);
                        newPartido.equipo_2 = myReader.GetInt32(5);
                        newPartido.gol_eq2 = myReader.GetInt32(6);
                        newPartido.local_eq1 = myReader.GetInt32(7);
                        newPartido.local_eq2 = myReader.GetInt32(8);
                        newPartido.temporada = myReader.GetString(9);
                        newPartido.estado = myReader.GetString(10);
                        newPartido.fecha = myReader.GetString(11);
                        ListMatches.Add(newPartido);
                    }

                    myReader.Close();
                }
            }

            return ListMatches;
        }
        public async Task<List<EntityTeam>> SelectTeamsById(int id)
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            var ListTeams = new List<EntityTeam>();
            string query = @"select id, id_liga, nombre, estado from equipos where id_liga='" + id + "' and estado='ACTIVADO';";
          
           
            MySqlDataReader myReader;
            using (Connection)
            {
                //await _repository.Connection.OpenAsync();
                await Connection.OpenAsync();
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    var convertirfecha = new conversorFechas();
                    myReader = mycommand.ExecuteReader();

                    while (await myReader.ReadAsync())
                    {
                        var Firstid = new EntityTeam();
                        Firstid.id = myReader.GetInt32(0);
                        Firstid.id_liga = myReader.GetInt32(1);
                        Firstid.nombre = myReader.GetString(2);
                        Firstid.estado = myReader.GetString(3);
                        ListTeams.Add(Firstid);
                    }

                    myReader.Close();
                    // _repository.Connection.Close();
                }
            }

            return ListTeams;
            
        }

        public async Task<List<EntityLeague>> SelectLeagues()
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            var ListLeagues = new List<EntityLeague>();
            string query = @"select id, nombre, pais, continente, nro_fh_tor  from liga;";


            MySqlDataReader myReader;
            using (Connection)
            {
                //await _repository.Connection.OpenAsync();
                await Connection.OpenAsync();
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    var convertirfecha = new conversorFechas();
                    myReader = mycommand.ExecuteReader();

                    while (await myReader.ReadAsync())
                    {
                        var Firstid = new EntityLeague();
                        Firstid.id = myReader.GetInt32(0);
                        Firstid.nombre = myReader.GetString(1);
                        Firstid.pais = myReader.GetString(2);
                        Firstid.continente = myReader.GetString(3);
                        Firstid.num_dates = myReader.GetInt32(4);
                        ListLeagues.Add(Firstid);
                    }

                    myReader.Close();
                    // _repository.Connection.Close();
                }
            }

            return ListLeagues;

        }

        public async Task<int> MaxDate(int id)
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            int result = 0;
            string query = @"SELECT max(nro_fecha) as max FROM proyecto.calendario where id_liga ='" + id + "';";
            MySqlDataReader myReader;
            using (Connection)
            {
                //await _repository.Connection.OpenAsync();
                await Connection.OpenAsync();
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    var convertirfecha = new conversorFechas();
                    myReader = mycommand.ExecuteReader();
                
        
                        while (await myReader.ReadAsync())
                        {
                            string? TestIsNull = myReader["max"].ToString();
                            if (TestIsNull == "")
                            {
                                result = 0;
                            }
                            else 
                            {
                                result = myReader.GetInt32(0);
                            }
                            
                        }
                    
            
                    myReader.Close();
                    // _repository.Connection.Close();
                }
            }
            return result;
        }

        

        public async Task<EntityRequest> InsertMactchesDate(EntityMatch entityMatch)
        {
            var objConnection = new RepositoryMysql(_configuration);
            MySqlConnection Connection = objConnection.Connection();
            /*var ListTeams = new List<EntityTeam>();
            string queryv = "insert into proyecto.users(id,nick,name,email,password,rol) values(null,'"
                 + entityUser.nick + "','" + entityUser.name + "','" + entityUser.email + "','" +
                 entityUser.password + "','" + entityUser.rol + "');";*/

            /* string query = "INSERT INTO proyecto.calendario(id, nro_fecha, id_liga, equipo_1, gol_eq1,equipo_2," +
                            "gol_eq2, local_eq1, local_eq2, estado, temporada,fecha) VALUES (null,'"+entityMatch.nro_fecha+ "','" +
                            entityMatch.id_liga + "','" + entityMatch.equipo_1 + "','" + entityMatch.gol_eq1 + "','" + entityMatch.equipo_2+
                            "','" + entityMatch.gol_eq2 + "','" + entityMatch.local_eq1 + "','" + entityMatch.local_eq2 + "','" +
                            entityMatch.estado + "','" + entityMatch.temporada + "','" + entityMatch.fecha + "');";*/

            string query = @"INSERT INTO proyecto.calendario(id, nro_fecha, id_liga, equipo_1, gol_eq1,equipo_2," +
                            "gol_eq2, local_eq1, local_eq2, estado, temporada,fecha) VALUES (@id,@nro_fecha, @id_liga, @equipo_1, " +
                            "@gol_eq1, @equipo_2," + "@gol_eq2, @local_eq1, @local_eq2, @estado, @temporada, @fecha);";

            //MySqlDataReader myReader;
            
            using (Connection)
            {
                await Connection.OpenAsync();
                using (MySqlCommand mycommand = new MySqlCommand(query, Connection))
                {
                    
                     mycommand.Parameters.AddWithValue("@id",0);
                     mycommand.Parameters.AddWithValue("@nro_fecha", entityMatch.nro_fecha);
                     mycommand.Parameters.AddWithValue("@id_liga", entityMatch.id_liga);
                     mycommand.Parameters.AddWithValue("@equipo_1", entityMatch.equipo_1);
                     mycommand.Parameters.AddWithValue("@gol_eq1", entityMatch.gol_eq1);
                     mycommand.Parameters.AddWithValue("@equipo_2", entityMatch.equipo_2);
                     mycommand.Parameters.AddWithValue("@gol_eq2", entityMatch.gol_eq2);
                     mycommand.Parameters.AddWithValue("@local_eq1", entityMatch.local_eq1);
                     mycommand.Parameters.AddWithValue("@local_eq2", entityMatch.local_eq2);
                     mycommand.Parameters.AddWithValue("@estado", entityMatch.estado);
                     mycommand.Parameters.AddWithValue("@temporada", entityMatch.temporada);
                     mycommand.Parameters.AddWithValue("@fecha", entityMatch.fecha);
                    try
                    {
                        await mycommand.ExecuteNonQueryAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                    
                    
                    /*var convertirfecha = new conversorFechas();
                    while (await myReader.ReadAsync())
                    {
                        var Firstid = new EntityTeam();
                        Firstid.id = myReader.GetInt32(0);
                        Firstid.id_liga = myReader.GetInt32(1);
                        Firstid.nombre = myReader.GetString(2);
                        Firstid.estado = myReader.GetString(3);
                        ListTeams.Add(Firstid);
                    }*/


                    // _repository.Connection.Close();
                }
            }
            await Connection.CloseAsync();
            var resultQuery = new EntityRequest()
            {
                request = true,
                msg = "Los datos fueron guardados correctamente"
            };
            //await myReader.CloseAsync();

            return resultQuery;

        }
    }
}
