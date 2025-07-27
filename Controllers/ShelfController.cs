using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Library.ModelView;
using Library.Dto;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;


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

        public IActionResult ShelfPage(int shelfPage = 1)
        {
            return View(new LibraryListViewModel
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
        }


        [HttpGet]
        public async Task<IActionResult> UpdateShelf(int id)
        {
            var shelf = await _shelfRepository.GetShelfById(id);
            var shelfDto = shelf.Adapt<ShelfDto>();
            if (shelfDto == null)
            {
                return NotFound("Shelf not found.");
            }
            return View(shelfDto);
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

            bool shelfExists = await _shelfRepository.IsShelfExist(shelfDto.Name);

            if (shelfExists)
            {
                ModelState.AddModelError("Name", "Shelf name must be unique.");
                return View(shelfDto); 
            }

            await _shelfRepository.AddShelf(shelfDto);
            return RedirectToAction("ShelfPage");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            var shelf = await _shelfRepository.SoftDeleteShelf(id);
            if (shelf == null)
            {
                return BadRequest("Shelf Not Found");
            }

            return RedirectToAction("ShelfPage");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateShelf(ShelfDto shelfDto)
        {
            if (shelfDto == null)
            {
                return BadRequest("Shelf data is invalid.");
            }

            bool shelfExists = await _shelfRepository.IsShelfExist(shelfDto.Name);


            if (shelfExists)
            {
                ModelState.AddModelError("Name", "Shelf name must be unique.");
                return View(shelfDto); 
            }

            var updatedShelf = await _shelfRepository.UpdateShelf(shelfDto);
            if (updatedShelf == null)
            {
                return NotFound("Shelf not found.");
            }

            return RedirectToAction(nameof(ShelfPage));
        }
        [HttpGet]
        public IActionResult AddShelf()
        {
            return View();
        }
     

    }
}
