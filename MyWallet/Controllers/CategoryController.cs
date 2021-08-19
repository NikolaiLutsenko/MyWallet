using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models;
using MyWallet.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
            return View(new CreateCategory
            {
                Categories = new SelectList(categories, "Id", "Name")
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(CreateCategory model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Create));

            if (!model.Id.HasValue)
                await _categoryService.Add(model.Name, model.Color, model.ParrentId);
            else
                await _categoryService.Update(model.Id.Value, model.Name, model.Color, model.ParrentId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categories = await _categoryService.GetAll();
            var editCategory = categories.SingleOrDefault(x => x.Id == id);

            return View(nameof(Create), new CreateCategory
            {
                Id = editCategory.Id,
                Color = editCategory.Color,
                Name = editCategory.Name,
                Categories = new SelectList(categories, "Id", "Name", editCategory.Parrent?.Id)
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
