using API.Utils;
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
            // HashingUtils.Hash("EliseGray123456!", out string salt1, out string hash1);
            // HashingUtils.Hash("AliceGlass12345!", out string salt2, out string hash2);
            // HashingUtils.Hash("timotheechalamet123!", out string salt3, out string hash3);
            // HashingUtils.Hash("CielPhantomhive123!", out string salt4, out string hash4);
            // HashingUtils.Hash("EdogawaRanpo123!", out string salt5, out string hash5);
            // modelBuilder.Entity<User>().HasData(
            //     new User() {
            //         Id = 1, Name = "Elise Gray", Email = "elise_gray@example.com", Role = Role.AUTHOR, PasswordSalt = salt1, PasswordHash = hash1
            //     },
            //     new User() {
            //         Id = 2, Name = "Alice Glass", Email = "alice_glass@example.com", Role = Role.AUTHOR, PasswordSalt = salt2, PasswordHash = hash2
            //     },
            //     new User() {
            //         Id = 3, Name = "Timothee Chalamet", Email = "timothee_chalamet@example.com", Role = Role.AUTHOR, PasswordSalt = salt3, PasswordHash = hash3
            //     },
            //     new User() {
            //         Id = 4, Name = "Ciel Phantomhive", Email = "ciel_phantomhive@example.com", Role = Role.EDITOR, PasswordSalt = salt4, PasswordHash = hash4
            //     },
            //     new User() {
            //         Id = 5, Name = "Edogawa Ranpo", Email = "edogawa_ranpo@example.com", Role = Role.READER, PasswordSalt = salt5, PasswordHash = hash5
            //     }
            // );

            // Topics
            modelBuilder.Entity<Topic>().HasData(
                new Topic() {Id = 1, TopicName = "Mental health"}
            );
        }
    }
}