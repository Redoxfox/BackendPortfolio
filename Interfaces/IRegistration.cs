using ServerAPI.Models;
using ServerAPI.Models.EntitiesUsers;

namespace ServerAPI.Interfaces
{
    public interface IRegistration
    {
        Task<EntityRequest> InsertNewUser(EntityUser entityUser); 
    }
}
