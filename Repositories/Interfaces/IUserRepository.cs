using ContactSystem.Models;

namespace ContactSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserModel> FindByEmail(string email);
        public Task<List<UserModel>> FindAll();
        public Task<UserModel> FindById(int id);
        public Task<UserModel> Insert(UserModel model);
    }
}
