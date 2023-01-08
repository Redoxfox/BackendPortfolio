using Microsoft.AspNetCore.Mvc;
using ServerAPI.Interfaces;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesUsers;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
     
        private readonly IRegistration _registration;
        private readonly ILogin _login;
    

        public UsersController(IRegistration registration, ILogin login)
        {
            _registration = registration;
            _login = login;
        }
        [HttpPost("users")]
        public async Task<EntityRequest> AddUser([FromBody] EntityUser entityUser)
        {
            EntityRequest result = await _registration.InsertNewUser(entityUser);

            return result;
        }

        [HttpPost("login")]
        //[Route("api/login")]
        public async Task<EntityToken> LoginUser([FromBody] EntityLogin entityLogin)
        {
            string result = await _login.SelectDataUser(entityLogin);
            var entityToken = new EntityToken();
            entityToken.token = result;
            return entityToken;

        }
    }
}
