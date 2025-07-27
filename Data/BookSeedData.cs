using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class BookSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            LibraryDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<LibraryDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                new Book
                {
                    Name = "Calculas1",
                    Price = 20,
                    ShelfId = 1
                },
                new Book
                {
                    Name = "Calculas2",
                    Price = 22,
                    ShelfId = 1
                },
                new Book
                {
                    Name = "Numarical",
                    Price = 87,
                    ShelfId = 1
                },
                new Book
                {
                    Name = "Theory",
                    Price = 36,
                    ShelfId = 1
                },
                new Book
                {
                    Name = "Calculas3",
                    Price = 128,
                    ShelfId = 1
                },
                new Book
                {
                    Name = "Linear Aljebra",
                    Price = 33,
                    ShelfId = 1
                },
                new Book
                {
                    Name = "Physics1 ",
                    Price = 25,
                    ShelfId = 2
                },
                new Book
                {
                    Name = "Physics2",
                    Price = 32,
                    ShelfId = 2
                },
                new Book
                {
                    Name = "Physics3",
                    Price = 24,
                    ShelfId = 2
                }

                );
                context.SaveChanges();
            }
        }
    }
}