using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Library.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shelf>()
                 .HasMany(s => s.Books)
                 .WithOne(b => b.Shelf)
                 .HasForeignKey(e => e.ShelfId);

            modelBuilder.Entity<Shelf>()
                .HasQueryFilter(s => !s.IsNotActive);
            modelBuilder.Entity<Book>()
                .HasQueryFilter(b => !b.IsNotActive);
        }


    }
}
