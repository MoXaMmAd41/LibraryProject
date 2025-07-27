using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryDbContext : IdentityDbContext<User> 
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Book> Books { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Shelf>()
                 .HasMany(s => s.Books)
                 .WithOne(b => b.Shelf)
                 .HasForeignKey(e => e.ShelfId);

            modelBuilder.Entity<Shelf>()
                .HasQueryFilter(s => !s.IsNotActive);
            modelBuilder.Entity<Book>()
                .HasQueryFilter(b => !b.IsNotActive);

            modelBuilder.Entity<Shelf>()
            .HasIndex(s => s.Name)
            .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(b => b.Email)
               .IsUnique();

            modelBuilder.Entity<User>()
                .ToTable(("Users"));
        }
    }
}