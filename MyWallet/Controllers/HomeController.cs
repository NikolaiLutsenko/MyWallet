using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHistoryLinesService historyLinesService, ICategoryService categoryService, ILogger<HomeController> logger)
        {
            _historyLinesService = historyLinesService;
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromServices] IChartService chartService)
        {
            var from = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1);
            var to = DateTime.Now.Date;
            var chartInfo = await chartService.GetChart(from, to);
            
            var categories = (await _categoryService.GetAll())
                .Where(x => x.Parrent == null && x.Child.Any())
                .ToArray();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(new ChartModel
            {
                DataSetLabel = chartInfo.DataSetLabel,
                Labels = chartInfo.Labels,
                Colors = chartInfo.Colors,
                Amounts = chartInfo.Amounts,
                From = from,
                To = to
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            [FromServices] IChartService chartService,
            DateTime? from,
            DateTime? to,
            Guid? categoryId)
        {
            var chartInfo = await chartService.GetChart(from, to, categoryId);
            return Ok(new ChartModel
            {
                DataSetLabel = chartInfo.DataSetLabel,
                Labels = chartInfo.Labels,
                Colors = chartInfo.Colors,
                Amounts = chartInfo.Amounts
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
