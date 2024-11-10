using ContactSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Data
{
    public class ContactSystemDBContext : DbContext
    {
        public ContactSystemDBContext(DbContextOptions<ContactSystemDBContext> options) : base(options)
        {}

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
