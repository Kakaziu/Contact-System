using ContactSystem.Models;
using System.Text.Json;

namespace ContactSystem.Helpers
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void CreateUserSession(UserModel user)
        {
            string value = JsonSerializer.Serialize(user);

            _contextAccessor.HttpContext.Session.SetString("LoggedUserSession", value);
        }

        public UserModel GetUserSession()
        {
            string session = _contextAccessor.HttpContext.Session.GetString("LoggedUserSession");

            if (string.IsNullOrEmpty(session)) return null;

            UserModel user = JsonSerializer.Deserialize<UserModel>(session);

            return user;
        }

        public void RemoveUserSession()
        {
            _contextAccessor.HttpContext.Session.Remove("LoggedUserSession");
        }
    }
}
