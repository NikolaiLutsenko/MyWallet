using AutoMapper;
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
    public class HistoryLinesController : Controller
    {
        private readonly IHistoryLinesService _historyLinesService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public HistoryLinesController(IHistoryLinesService historyLinesService, ICategoryService categoryService, IMapper mapper)
        {
            _historyLinesService = historyLinesService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var from = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1);
            var to = DateTime.Now.Date;
            var userId = User.GetCurrentUserId();
            var lines = await _historyLinesService.GetAll(userId);

            var categories = (await _categoryService.GetAll(userId))
                .Where(x => x.Parrent == null && x.Child.Any())
                .ToArray();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(new HistoryLinesModel
            {
                From = from,
                To = to,
                Lines = _mapper.Map<HistoryLineModel[]>(lines)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            DateTime? from,
            DateTime? to,
            Guid? categoryId)
        {
            var userId = User.GetCurrentUserId();
            var lines = await _historyLinesService.GetAll(userId, from, to, categoryId);

            return PartialView("_HistoryLines", _mapper.Map<HistoryLineModel[]>(lines));
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
