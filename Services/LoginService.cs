using Microsoft.IdentityModel.Tokens;
using ServerAPI.Helpers;
using ServerAPI.Interfaces;
using ServerAPI.Models;
using ServerAPI.Models.EntitiesUsers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerAPI.Services
{
    public class LoginService:ILogin
    {
        private readonly IQueriesUser _queriesUser;
        private readonly IValidations _validations;
        private readonly IConfiguration _configuration;

        public LoginService(IQueriesUser queriesUser,  IValidations validations, IConfiguration configuration)
        {
            _queriesUser = queriesUser;
            _validations = validations;
            _configuration = configuration;
        }
        public async Task<string> SelectDataUser(EntityLogin entityLogin)
        {
            var RequestQueryUser = new EntityUser();
            var DataLogin = new EntityLogin();
            var ObjEncrypt = new Encrypt();

            DataLogin.email = entityLogin.email;
            DataLogin.password = entityLogin.password;
            RequestQueryUser= await _queriesUser.SelectUserDb(DataLogin);
            string PasswordClient = ObjEncrypt.GetSHA256(DataLogin.password);



            if (RequestQueryUser.password == PasswordClient)
            {
                string objToken = GetToken(entityLogin);
                return objToken;
            }
            else
            {
                return "false";
            }   
        }

        private string GetToken(EntityLogin entityLogin)
        {
            string keyString = _configuration.GetSection("AppSettings:VisitorSecretKey").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var KeyJwt = Encoding.ASCII.GetBytes(keyString);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                            new Claim(ClaimTypes.Email, entityLogin.email)
                    }

                 ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyJwt), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

   
}
