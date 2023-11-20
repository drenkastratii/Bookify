using Bookify.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action"},
                new Category { Id=2, Name="Drama"},
                new Category { Id=3, Name="SciFI"}

                );
            modelBuilder.Entity<Author>().HasData(
                new Author { Id=1, FullName="Abby Muscles" },
                new Author { Id=2, FullName="Nancy Hoover"},
                new Author { Id=3, FullName="Billy Spark"},
                new Author { Id=4, FullName="Laura Phantom"},
                new Author { Id=5, FullName="Ron Parker"},
                new Author { Id=6, FullName="Julian Button"}
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id=1, Title="Cotton Candy", Description= "This is the Description", Price=15},
                new Book { Id=2, Title="Cotton Candy", Description="This is the Description", Price=18},
                new Book { Id=3, Title="Fortune of Time", Description="This is the Description", Price=22},
                new Book { Id=4, Title="Leaves and Wonders", Description="This is the Description", Price=9},
                new Book { Id=5, Title="Rock in The Ocean", Description="This is the Description", Price=11},
                new Book { Id=6, Title="Vanish in The Sunset", Description="This is the Description", Price=17}

               );


        }

    }
}
