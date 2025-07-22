using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class ShelfSeedData
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
            if (!context.Shelves.Any())
            {
                context.Shelves.AddRange(
                new Shelf
                {
                    Name = "MathShelf"
                    
                
                },
                new Shelf
                {
                    Name = "ScienceShelf"
                }
              
                );
                context.SaveChanges();
            }
        }
    }
}
