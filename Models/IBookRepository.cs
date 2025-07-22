using Library.Dto;

namespace Library.Models
{
    public interface IBookRepository
    {
        IQueryable<Book> GetBooks();
        Task AddBook(BookDto bookDto);
        Task<Book> DeleteBook(int id);
        Task<Book> GetBookById(int id);
        Task<Book> UpdateBook(BookDto bookDto);
    }
}
