using ContactSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ContactSystem.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string session = HttpContext.Session.GetString("LoggedUserSession");

            if(string.IsNullOrEmpty(session) ) return View();

            UserModel user = JsonSerializer.Deserialize<UserModel>(session);

            return View(user);
        }
    }
}
