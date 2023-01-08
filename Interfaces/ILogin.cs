using ServerAPI.Models.EntitiesUsers;

namespace ServerAPI.Interfaces
{
    public interface ILogin
    {
        Task<string> SelectDataUser(EntityLogin entityLogin);
    }
}
