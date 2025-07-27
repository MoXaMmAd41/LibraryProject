using Library.Data;
using Library.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly LibraryDbContext _context;

        public ShelfRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public IQueryable<Shelf> GetShelves() => _context.Shelves;

        public async Task<bool> IsShelfExist(string name)
        {
            return await _context.Shelves.AnyAsync(s => s.Name == name && s.IsNotActive == false);
        }

        public async Task<Shelf?> GetShelfById(int id)
        {
            return await _context.Shelves.FindAsync(id);
        }

        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _context.Shelves.ToListAsync();
        }

        public async Task AddShelf(ShelfDto shelfDto)
        {
            var shelf = shelfDto.Adapt<Shelf>();
            shelf.CreatedDate = DateTime.Now;
            await _context.Shelves.AddAsync(shelf);
            await _context.SaveChangesAsync();
        }

        public async Task<Shelf?> SoftDeleteShelf(int id)
        {
            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf == null) return null;

            shelf.IsNotActive = true;
            await _context.SaveChangesAsync();

            return shelf;
        }

        public async Task<Shelf?> UpdateShelf(ShelfDto shelfDto)
        {
            var existingShelf = await _context.Shelves.FindAsync(shelfDto.Id);
            if (existingShelf == null) return null;

            existingShelf = shelfDto.Adapt(existingShelf);
            existingShelf.UpdatedDate = DateTime.Now;

            _context.Shelves.Update(existingShelf);
            await _context.SaveChangesAsync();

            return existingShelf;
        }
    }
}
