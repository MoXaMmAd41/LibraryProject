using Library.Dto;

namespace Library.Models
{
    public interface IShelfRepository
    {
        IQueryable<Shelf> GetShelves();
        Task<Shelf> GetShelfById(int id);
        Task<List<Shelf>> GetAllShelves();
        Task AddShelf(ShelfDto shelfDto);
        Task<Shelf> SoftDeleteShelf(int id);
        Task<Shelf> UpdateShelf(ShelfDto shelfDto);
    }
}
