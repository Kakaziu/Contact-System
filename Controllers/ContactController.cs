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
            await _contactRepository.Insert(contact);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactModel contact, int id)
        {
            try
            {
                contact.Id = id;
                await _contactRepository.Update(contact, id);

                return RedirectToAction(nameof(Index));
            } catch(Exception)
            {
                return View();
            }
        }

        public IActionResult ConfirmDelete()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contactRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }catch(Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
