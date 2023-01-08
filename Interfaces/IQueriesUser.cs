using ServerAPI.Models;
using ServerAPI.Models.EntitiesUsers;

namespace ServerAPI.Interfaces
{
    public interface IQueriesUser
    {
        Task<EntityRequest> InsertUserDb(EntityUser entityUser);
        Task<EntityUser> SelectUserDb(EntityLogin entityLogin);
    }
}
