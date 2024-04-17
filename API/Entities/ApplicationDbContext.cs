using Microsoft.EntityFrameworkCore;

namespace API.Entities {
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users {get; set;}
    }
}