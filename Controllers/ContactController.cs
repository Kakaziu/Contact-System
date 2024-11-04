using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository) 
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactRepository.FindAll();

            return View(contacts);
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
