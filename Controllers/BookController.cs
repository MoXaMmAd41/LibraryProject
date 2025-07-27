using Library.Data;
using Library.Dto;
using Library.Models;
using Library.ModelView;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


namespace Library.Controllers
{

    public class BookController : Controller
    {
        private IBookRepository _bookRepository;


        public BookController(IBookRepository repo)
        {
            _bookRepository = repo;
        }
        int PageSize = 3;

        public ViewResult MaterialPage(int shelfId, int bookPage = 1) =>

            View(new LibraryListViewModel
            {

                ShelfId = shelfId,
                Books = _bookRepository.GetBooks()
                .OrderBy(p => p.Id)
                .Where(p => p.ShelfId == shelfId && p.IsNotActive == false)
                .Skip((bookPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {

                    CurrentPage = bookPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _bookRepository.GetBooks().Count()
                }

            });


        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetBooks().ToListAsync();
            return View(books);
        }
        [HttpGet]
        public IActionResult AddBook(int shelfId)
        {
            ViewBag.ShelfId = shelfId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var existingBook = await _bookRepository.IsBookExist(bookDto.Name);
                

                if (existingBook)
                {
                    ModelState.AddModelError("Name", "A book with this name already exists on this shelf.");
                    ViewBag.ShelfId = bookDto.ShelfId;
                    return View(bookDto);
                }


                await _bookRepository.AddBook(bookDto);
                return RedirectToAction("MaterialPage", new { shelfId = bookDto.ShelfId });
            }

            ViewBag.ShelfId = bookDto.ShelfId;
            return View(bookDto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.DeleteBook(id);
            if (book == null)
            {
                return BadRequest("Book Not Found");
            }

            return RedirectToAction("MaterialPage", new { shelfId = book.ShelfId });
        }
        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {

            var book = await _bookRepository.GetBookById(id);
            var bookDto = book.Adapt<BookDto>();
            if (bookDto == null)
            {
                return NotFound("Book not found.");
            }

            ViewBag.ShelfId = bookDto.ShelfId;
            return View(bookDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest("Book data is invalid.");
            }

            var updatedBook = await _bookRepository.UpdateBook(bookDto);
            if (updatedBook == null)
            {
                return NotFound("Book not found.");
            }

            return RedirectToAction("MaterialPage", new { shelfId = updatedBook.ShelfId });
        }
    }

}

