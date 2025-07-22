using Library.Data;
using Library.Dto;
using Mapster;

namespace Library.Models
{
    public class BookRepository:IBookRepository
    {
        private LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> GetBooks() => _context.Books;

        public async Task AddBook(BookDto bookDto)
        {
            var book = bookDto.Adapt<Book>();
            book.CreatedDate = DateTime.Now;
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
        public async Task<Book> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            book.IsNotActive = true;
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book?> UpdateBook(BookDto bookDto)
        {
            var existingBook = await _context.Books.FindAsync(bookDto.Id);
            existingBook = bookDto.Adapt(existingBook);
            existingBook.UpdatedDate = DateTime.Now;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return existingBook;
        }
    }
}
