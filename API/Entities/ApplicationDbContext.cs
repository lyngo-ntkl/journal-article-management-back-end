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
            InitializeTestData(modelBuilder);
        }

        private void InitializeTestData(ModelBuilder modelBuilder) {
            // Users
            modelBuilder.Entity<User>().HasData(
                new User() {
                    Id = 1, Name = "Elise Gray", Email = "elise_gray@example.com", Role = Role.AUTHOR, Password = "EliseGray123!"
                },
                new User() {
                    Id = 2, Name = "Alice Glass", Email = "alice_glass@example.com", Role = Role.AUTHOR, Password = "AliceGlass123!"
                },
                new User() {
                    Id = 3, Name = "Timothee Chalamet", Email = "timothee_chalamet@example.com", Role = Role.AUTHOR, Password = "timotheechalamet123!"
                },
                new User() {
                    Id = 4, Name = "Ciel Phantomhive", Email = "ciel_phantomhive@example.com", Role = Role.EDITOR, Password = "CielPhantomhive123!"
                },
                new User() {
                    Id = 5, Name = "Edogawa Ranpo", Email = "edogawa_ranpo@example.com", Role = Role.READER, Password = "EdogawaRanpo123!"
                }
            );

            // Topics
            modelBuilder.Entity<Topic>().HasData(
                new Topic() {Id = 1, TopicName = "Mental health"}
            );
        }
    }
}