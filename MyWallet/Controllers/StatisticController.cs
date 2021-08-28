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
    public class StatisticController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;

        public StatisticController(ICategoryService categoryService, IStatisticService statisticService, IMapper mapper)
        {
            _categoryService = categoryService;
            _statisticService = statisticService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();
            var range = RangeTypes.Day;
            var (currentRange, prevRange) = GetRanges(range);

            var statisticItems = await _statisticService.GetStatistic(userId, prevRange, currentRange);
            var categories = (await _categoryService.GetAll(userId))
                .Where(x => x.Parrent == null && x.Child.Any())
                .ToArray();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(_mapper.Map<StatisticItemModel[]>(statisticItems));
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            RangeTypes rangeType,
            Guid? categoryId)
        {
            var (currentRange, prevRange) = GetRanges(rangeType);
            var userId = User.GetCurrentUserId();
            var statisticItems = await _statisticService.GetStatistic(userId, prevRange, currentRange, categoryId);

            return PartialView("_Statistic", _mapper.Map<StatisticItemModel[]>(statisticItems));
        }

        private static (DateRange thisRange, DateRange prevRange) GetRanges(RangeTypes range)
        {
            return range switch
            {
                RangeTypes.Day => (DateRange.Day, DateRange.PrevDay),
                RangeTypes.Week => (DateRange.Week, DateRange.PrevWeek),
                RangeTypes.Month => (DateRange.Month, DateRange.PrevMonth),
                _ => throw new NotImplementedException()
            };
        }
    }
}
