using ServerAPI.Helpers;
using ServerAPI.Interfaces;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesUsers;

namespace ServerAPI.Services
{
    public class RegistrationService:IRegistration
    {
        private readonly IQueriesUser _queriesUser;

        public RegistrationService(IQueriesUser queriesUser)
        {
            _queriesUser = queriesUser;
        }
        public async Task<EntityRequest> InsertNewUser(EntityUser entityUser)
        {
           var ObjEncrypt = new Encrypt();
           var entityUserEncript = new EntityUser();
           entityUserEncript.id=1;
           entityUserEncript.nick=entityUser.nick;
           entityUserEncript.name=entityUser.name;
           entityUserEncript.email=entityUser.email;
           entityUserEncript.password= ObjEncrypt.GetSHA256(entityUser.password);
           entityUserEncript.rol = entityUser.rol;
           EntityRequest request = await _queriesUser.InsertUserDb(entityUserEncript);
           return request;
        }
    }
}
