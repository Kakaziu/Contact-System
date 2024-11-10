using ContactSystem.Data;
using ContactSystem.Models;
using ContactSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactSystemDBContext _context;

        public UserRepository(ContactSystemDBContext context)
        {
            _context = context;
        }

        public async Task<UserModel> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task<List<UserModel>> FindAll()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<UserModel> FindById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<UserModel> Insert(UserModel model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
