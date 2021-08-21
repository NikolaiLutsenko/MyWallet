using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Extensions;
using MyWallet.Models;
using MyWallet.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
    [Authorize]
    public class HistoryLinesController : Controller
    {
        private readonly IHistoryLinesService _historyLinesService;

        public HistoryLinesController(IHistoryLinesService historyLinesService)
        {
            _historyLinesService = historyLinesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();
            var lines = await _historyLinesService.GetAll(userId);
            return View(lines);
        }

        [HttpGet]
        public async Task<IActionResult> Create([FromServices] ICategoryService categoryService)
        {
            var userId = User.GetCurrentUserId();
            var categories = await categoryService.GetAll(userId);
            return View(new CreateHistoryLine
            {
                Categories = new SelectList(categories, "Id", "Name")
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHistoryLine model)
        {
            if (!ModelState.IsValid) return View(model);

            await _historyLinesService.AddCredit(model.Name, model.Amount, model.CategoryId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _historyLinesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
