using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ServerAPI.Helpers;
using ServerAPI.Interfaces;
using ServerAPI.Interfaces.InterfacesBets;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesBets;
using ServerAPI.Repositories;
using ServerAPI.Services.ServiciesBets;
using System.Data;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BetsController : Controller
    {
        
        private readonly IQueriesBets _queriesBets;

        public BetsController(IQueriesBets queriesBets)
        {
            _queriesBets = queriesBets;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetMatchesCalendar()
        {
            var ListMatches = new List<EntityMatch>();
            ListMatches = await _queriesBets.SelectMatches();
            return Ok(ListMatches);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTeamsLeague(int id)
        {
            var ListTeams = new List<EntityTeam>();
            ListTeams = await _queriesBets.SelectTeamsById(id);
            return Ok(ListTeams);
        }
        
        [HttpPost("AddMatchesDates")]
        //[Route("api/login")]
        public async Task<EntityRequest> AddMatchesDates([FromBody] List<EntityMatch> entityMatch)
        {
            var ObjService = new BetService(_queriesBets);
            var Result = new EntityRequest();
            var listma = new List<EntityMatch>();
            listma = entityMatch;
            Result = await ObjService.InsertMactchesDate(listma);
            return Result;
        }

        [HttpGet("GetLeagues")]
        //[Route("api/login")]
        public async Task<List<EntityLeague>> GetLeagues()
        {
            var ObjService = new BetService(_queriesBets);
            var Result = new List<EntityLeague>();
            Result = await ObjService.SelectLeagues();
            return Result;
        }

        [HttpPost("GetListDatesLeague")]
        //[Route("api/login")]
        public async Task<List<EntityLinksDate>> GetListDatesLeague([FromBody] EntityLinksDate RequestE)
        {
            var ObjService = new BetService(_queriesBets);
            var Result = new List<EntityLinksDate>();
            Result = await ObjService.ListDatesLeague(RequestE);
            return Result;
        }

        [HttpGet("GetListLeague")]
        //[Route("api/login")]
        public async Task<List<EntityLeague>> GetListLeague()
        {
            var ObjService = new BetService(_queriesBets);
            var Result = new List<EntityLeague>();
            Result = await ObjService.ListLeague();
            return Result;
        }
    }
}
