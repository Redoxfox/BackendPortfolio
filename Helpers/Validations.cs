using ServerAPI.Interfaces;

namespace ServerAPI.Helpers
{
    public class Validations:IValidations
    {
        public bool ExistUser(string username)
        {
            if(username == null) 
            {
                return false;
            }
            return true;
        }

        public bool ExistNick(string username)
        {
            if (username == null)
            {
                return false;
            }
            return true;
        }
    }
}
