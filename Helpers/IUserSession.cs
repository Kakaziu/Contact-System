using ContactSystem.Models;

namespace ContactSystem.Helpers
{
    public interface IUserSession
    {
        void CreateUserSession(UserModel user);
        void RemoveUserSession();
        UserModel GetUserSession();
    }
}
