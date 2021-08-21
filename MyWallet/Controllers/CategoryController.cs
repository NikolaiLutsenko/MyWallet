using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Extensions;
using MyWallet.Models;
using MyWallet.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
    [Authorize]
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
            var userId = User.GetCurrentUserId();
            var categories = await _categoryService.GetAll(userId);
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.GetCurrentUserId();
            var categories = await _categoryService.GetAll(userId);
            return View(new CreateCategory
            {
                Categories = new SelectList(categories, "Id", "Name")
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(CreateCategory model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Create));

            var userId = User.GetCurrentUserId();
            if (!model.Id.HasValue)
                await _categoryService.Add(userId, model.Name, model.Color, model.ParrentId);
            else
                await _categoryService.Update(userId, model.Id.Value, model.Name, model.Color, model.ParrentId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = User.GetCurrentUserId();
            var categories = await _categoryService.GetAll(userId);
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
            var userId = User.GetCurrentUserId();
            await _categoryService.Remove(userId, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
