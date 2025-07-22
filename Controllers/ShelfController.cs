using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Library.ModelView;
using Library.Dto;

namespace Library.Controllers
{
    public class ShelfController : Controller
    {
        private IShelfRepository _shelfRepository;


        public ShelfController( IShelfRepository repo)
        {
            _shelfRepository = repo;
        }

        int PageSize = 3;

        public ViewResult HomePage(int shelfPage = 1) =>
            View(new LibraryListViewModel
            {
                Shelves = _shelfRepository.GetShelves()
                .OrderBy(p => p.Id)
                .Skip((shelfPage - 1) * PageSize)
                .Where(p => p.IsNotActive == false)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = shelfPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _shelfRepository.GetShelves().Count()
                }
               
            });
        [HttpGet]
        public async Task<IActionResult> EditShelf(int id)
        {
            var shelf = await _shelfRepository.GetShelfById(id);
            if (shelf == null)
            {
                return NotFound("Shelf not found.");
            }
            return View(shelf);
        }
        [HttpGet]
        public async Task<IActionResult> GetShelfes()
        {
            var shelves = await _shelfRepository.GetAllShelves();
            return Ok(shelves); 
        }

        [HttpPost]
        public async Task<IActionResult> AddShelf(ShelfDto shelfDto)
        {
            if (shelfDto == null || string.IsNullOrEmpty(shelfDto.Name))
            {
                return BadRequest("Shelf data is invalid.");
            }

            await _shelfRepository.AddShelf(shelfDto);
            return RedirectToAction("HomePage");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            var shelf = await _shelfRepository.SoftDeleteShelf(id);
            if (shelf == null)
            {
                return BadRequest("Shelf Not Found");
            }

            return RedirectToAction("HomePage");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateShelf(ShelfDto shelfDto)
        {
            if (shelfDto == null)
            {
                return BadRequest("Shelf data is invalid.");
            }

            var updatedShelf = await _shelfRepository.UpdateShelf(shelfDto);
            if (updatedShelf == null)
            {
                return NotFound("Shelf not found.");
            }

            return RedirectToAction("HomePage");
        }
        [HttpGet]
        public IActionResult AddShelf()
        {
            return View();
        }

    }
}
