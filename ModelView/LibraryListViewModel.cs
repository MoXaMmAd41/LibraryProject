using Library.Models;

namespace Library.ModelView
{
    public class LibraryListViewModel
    {

        public int ShelfId { get; set; }
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
        public IEnumerable<Shelf> Shelves { get; set; } = Enumerable.Empty<Shelf>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
