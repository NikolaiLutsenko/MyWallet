using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MyWallet.Extensions;
using MyWallet.Models;
using MyWallet.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHistoryLinesService _historyLinesService;
        private readonly ICategoryService _categoryService;
        private readonly IChartService _chartService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHistoryLinesService historyLinesService, ICategoryService categoryService, IChartService chartService, ILogger<HomeController> logger)
        {
            _historyLinesService = historyLinesService;
            _categoryService = categoryService;
            _chartService = chartService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();
            var chartInfo = await _chartService.GetChart(userId, DateRange.Month);

            var categories = (await _categoryService.GetAll(userId))
                .Where(x => x.Parrent == null && x.Child.Any())
                .ToArray();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(new ChartModel(DateRange.Month)
            {
                DataSetLabel = chartInfo.DataSetLabel,
                Labels = chartInfo.Labels,
                Colors = chartInfo.Colors,
                Amounts = chartInfo.Amounts
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            DateTime from,
            DateTime to,
            Guid? categoryId)
        {
            var userId = User.GetCurrentUserId();
            var dateRange = new DateRange(from, to);
            var chartInfo = await _chartService.GetChart(userId, dateRange, categoryId);

            return Ok(new ChartModel(new DateRange(from, to))
            {
                DataSetLabel = chartInfo.DataSetLabel,
                Labels = chartInfo.Labels,
                Colors = chartInfo.Colors,
                Amounts = chartInfo.Amounts
            });
        }

        [HttpGet]
        public async Task<IActionResult> TableInfo(
            DateTime from,
            DateTime to,
            Guid? categoryId)
        {
            var userId = User.GetCurrentUserId();
            var dateRange = new DateRange(from, to);
            var chartInfo = await _chartService.GetChart(userId, dateRange, categoryId);
            var allTransactions =  await _historyLinesService.GetAll(userId, new DateRange(from, to), categoryId);
            var groupedTransactions = allTransactions
                .GroupBy(x => x.Date.Date)
                .Select(x => x.Sum(line => line.Amount))
                .OrderBy(x => x)
                .ToArray();
            var average = allTransactions.Sum(x => x.Amount) / groupedTransactions.Length;

            return PartialView("_Table", new ChartModel(new DateRange(from, to))
            {
                DataSetLabel = chartInfo.DataSetLabel,
                Labels = chartInfo.Labels,
                Colors = chartInfo.Colors,
                Amounts = chartInfo.Amounts,
                AveragePerDay = average,
                Mediana = groupedTransactions.Length % 2 == 0
                    ? (groupedTransactions[groupedTransactions.Length / 2] + groupedTransactions[(groupedTransactions.Length / 2) + 1]) / 2
                    : groupedTransactions[groupedTransactions.Length / 2]
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
