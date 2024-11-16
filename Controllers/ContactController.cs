using ContactSystem.Helpers;
using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserSession _userSession;

        public ContactController(IContactRepository contactRepository, IUserSession session) 
        {
            _contactRepository = contactRepository;
            _userSession = session;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userSession.GetUserSession();

            if (user == null) return RedirectToAction("Index", "Home");

            var contacts = await _contactRepository.FindAll();
            var userContacts = contacts.Where(x => x.UserId == user.Id).ToList();

            return View(userContacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactModel contact)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var user = _userSession.GetUserSession();

                    contact.UserId = user.Id;

                    await _contactRepository.Insert(contact);
                    TempData["SuccessMessage"] = "Contato cadastrado.";
                    return RedirectToAction(nameof(Index));
                }


                return View(contact);
            } catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Ops, tivemos um erro ao cadastrar seu contato, confira os detalhes do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactRepository.FindById(id);

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactModel contact, int id)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    contact.Id = id;
                    await _contactRepository.Update(contact, id);
                    TempData["SuccessMessage"] = "Contato alterado.";
                    return RedirectToAction(nameof(Index));
                }

                return View(contact);
            } catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Ops, tivemos um erro ao alterar seu contato, confira os detalhes do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var contact = await _contactRepository.FindById(id);

            return View(contact);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isDeleted = await _contactRepository.Delete(id);

                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Contato apagado.";
                    return RedirectToAction(nameof(Index));
                } else
                {
                    TempData["ErrorMessage"] = "Não foi possível apagar o contato.";
                    return RedirectToAction(nameof(Index));
                }
            }catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Ops, tivemos um erro ao apagar seu contato, confira os detalhes do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
