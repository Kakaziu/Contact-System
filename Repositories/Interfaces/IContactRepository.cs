using ContactSystem.Models;

namespace ContactSystem.Repositories.Interfaces
{
    public interface IContactRepository
    {
        public Task<List<ContactModel>> FindAll();
        public Task<ContactModel> FindById(int id);
        public Task<ContactModel> Insert(ContactModel model);
        public Task<ContactModel> Update(ContactModel model, int id);
        public Task<bool> Delete(int id);
    }
}
