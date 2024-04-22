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
            modelBuilder.Entity<Reference>().HasKey(reference => new {reference.ArticleId, reference.ReferenceArticleId});
            modelBuilder.Entity<Reference>()
                .HasOne(reference => reference.Article)
                .WithMany(article => article.CitationBy)
                .HasForeignKey(reference => reference.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Reference>()
                .HasOne(reference => reference.ReferenceArticle)
                .WithMany(article => article.References)
                .HasForeignKey(reference => reference.ReferenceArticleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}