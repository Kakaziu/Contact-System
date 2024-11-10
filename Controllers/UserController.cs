using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.FindAll();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userRepository.Insert(user);

                    return View(user);
                }

                return View();
            } catch (Exception)
            {
                return View();
            }    
        }
    }
}
