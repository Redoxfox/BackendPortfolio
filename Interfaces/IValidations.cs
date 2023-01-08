namespace ServerAPI.Interfaces
{
    public interface IValidations
    {
        bool ExistUser(string username);
        bool ExistNick(string username);
    }
}
