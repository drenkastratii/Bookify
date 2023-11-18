using BookifyWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookifyWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action"},
                new Category { Id=2, Name="Drama"},
                new Category { Id=3, Name="SciFI"}

                );
            modelBuilder.Entity<Author>().HasData(
                new Author { Id=1, FullName="Stephen King" },
                new Author { Id=2, FullName="George Orwell"},
                new Author { Id=3, FullName="Frank Herbert"}
                );
        }

    }
}
