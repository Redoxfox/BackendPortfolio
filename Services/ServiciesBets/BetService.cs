using Microsoft.AspNetCore.Mvc;
using ServerAPI.Interfaces.InterfacesBets;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesBets;

namespace ServerAPI.Services.ServiciesBets
{
    public class BetService
    {
        private readonly IQueriesBets _queriesBets;

        public BetService(IQueriesBets queriesBets)
        {
            _queriesBets = queriesBets;
        }


        [HttpGet]
        public async Task<List<EntityMatch>> SelectMatches()
        {
            var ListMatches = new List<EntityMatch>();
            ListMatches = await _queriesBets.SelectMatches();
            return ListMatches;
        }

        [HttpGet]
        public async Task<List<EntityLeague>> SelectLeagues()
        {
            var ListLeagues = new List<EntityLeague>();
            ListLeagues = await _queriesBets.SelectLeagues();
            return ListLeagues;
        }

        [HttpGet("{id:int}")]
        public async Task<List<EntityTeam>> SelectTeamsById(int id)
        {
            var ListTeams = new List<EntityTeam>();
            ListTeams = await _queriesBets.SelectTeamsById(id);
            return ListTeams;
        }

        
        public async Task<EntityRequest> InsertMactchesDate(List<EntityMatch> entityMatch)
        {
            int MaxDate = 0;
            var Result = new EntityRequest();
            MaxDate = await _queriesBets.MaxDate(entityMatch[0].id_liga);
            if (entityMatch[0].nro_fecha > MaxDate)
            {
                foreach (EntityMatch item in entityMatch)
                {
                    Result = await _queriesBets.InsertMactchesDate(item);
                }
                return Result;
            }
            else
            {
                Result.request = false;
                Result.msg = "Los datos de esta fecha ya fueron registrados.";
                return Result; 
            }
        }

        public async Task<List<EntityLinksDate>> ListDatesLeague(EntityLinksDate LinksDate)
        {
            //var Result = new EntityRequest();
            var ListLeagues = new List<EntityLeague>();
            ListLeagues = await _queriesBets.SelectLeagues();

            var query_where1 = from a in ListLeagues
                               where a.id==1
                               select a;
            var query_where2 = ListLeagues.Single(a => a.id== LinksDate.idLiga);
            int numDates= query_where2.num_dates;
            var listResquest = new List<EntityLinksDate>();
            int cont = 0;
            
            for(int i = 0; i < numDates; i++)
            {
                cont += 1;
                string newUrl = "https:";
                string[] subs = LinksDate.link.Split('/');
                for(int j = 0; j < subs.Length; j++)
                {
                    if (j < subs.Length-1 && j>0)
                    {
                        newUrl += "/" + subs[j];
                    }

                    if (j == subs.Length - 1)
                    {
                        newUrl += "/" + "jornada" + cont.ToString();
                    }
                  
                }
                
                var objRequest = new EntityLinksDate
                {
                   idLiga= LinksDate.idLiga,
                   link = newUrl
                };
                listResquest.Add(objRequest);
            }
            
           
            return listResquest;
        }

        public async Task<List<EntityLeague>> ListLeague()
        {
            var ListLeagues = new List<EntityLeague>();
            ListLeagues = await _queriesBets.SelectLeagues();
            return  ListLeagues;
        }
    }
}
