using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWallet.Models;
using MyWallet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
	public class HomeController : Controller
	{
        private readonly IHistoryLinesService _historyLinesService;
        private readonly ILogger<HomeController> _logger;

		public HomeController(IHistoryLinesService historyLinesService, ILogger<HomeController> logger)
		{
			_historyLinesService = historyLinesService;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			var lines = await _historyLinesService.GetAll();
			var grouped = lines.GroupBy(x => (x.Category.Name, x.Category.Color));

			return View(new ChartModel
			{
				Labels = grouped.Select(x => x.Key.Name).ToArray(),
				Colors = grouped.Select(x => x.Key.Color).ToArray(),
				Amounts = grouped.Select(x => x.Select(i => i.Amount).Sum()).ToArray()
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
