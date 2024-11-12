using ContactSystem.Helpers;
using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;

        public LoginController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            if (_userSession.GetUserSession() != null) return RedirectToAction("Index", "Contact");

            return View();
        }

        public IActionResult Logout()
        {
            _userSession.RemoveUserSession();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entry(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userRepository.FindByEmail(login.Email);

                    if (user != null)
                    {
                        if (user.ValidPassword(login.Password))
                        {
                            _userSession.CreateUserSession(user);
                            return RedirectToAction("Index", "Contact");
                        }
                    }

                    TempData["ErrorMessage"] = "Credenciais inválidas.";
                }

                return View(nameof(Index));
            }catch (Exception)
            {
                TempData["ErrorMessage"] = "Não foi possível realizar o login.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
