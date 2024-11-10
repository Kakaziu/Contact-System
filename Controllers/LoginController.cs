using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
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
