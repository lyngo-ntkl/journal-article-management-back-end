using Microsoft.EntityFrameworkCore;

namespace API.Entities {
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Article> Articles {get; set;}
        public DbSet<Topic> Topics {get; set;}
        public DbSet<Reference> References {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}