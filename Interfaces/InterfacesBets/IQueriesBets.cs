using ServerAPI.Models;
using ServerAPI.Models.EntitiesBets;

namespace ServerAPI.Interfaces.InterfacesBets
{
    public interface IQueriesBets
    {
        Task<List<EntityMatch>> SelectMatches();

        Task<List<EntityLeague>> SelectLeagues();
        Task<List<EntityTeam>> SelectTeamsById(int id);
        
        Task<EntityRequest> InsertMactchesDate(EntityMatch entityMatch);

        Task<int> MaxDate(int id);
    }
}
