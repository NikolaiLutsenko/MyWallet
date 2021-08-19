using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models;
using MyWallet.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
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
            var lines = await _historyLinesService.GetAll();
            return View(lines);
        }

        [HttpGet]
        public async Task<IActionResult> Create([FromServices] ICategoryService categoryService)
        {
            var categories = await categoryService.GetAll();
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
