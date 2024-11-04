using ContactSystem.Data;
using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactSystemDBContext _context;

        public ContactRepository(ContactSystemDBContext context)
        {
            _context = context;
        }

        public async Task<List<ContactModel>> FindAll()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<ContactModel> FindById(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if(contact == null) throw new Exception("Contato não encontrado.")
;
            return contact;
        }

        public async Task<ContactModel> Insert(ContactModel model)
        {
            await _context.Contacts.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<ContactModel> Update(ContactModel model, int id)
        {
            var contact = await FindById(id);

            if (contact == null) throw new Exception("Não foi possível alterar seu contato."); 
            
            contact.Name = model.Name;
            contact.Email = model.Email;
            contact.Phone = model.Phone;
            _context.Update(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<bool> Delete(int id)
        {
            var contact = await FindById(id);

            if (contact == null) throw new Exception("Não foi possível apagar seu contato.");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
