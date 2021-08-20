using Microsoft.EntityFrameworkCore;
using MyWallet.Data;
using MyWallet.Data.Entities;
using MyWallet.Services.Dtos;
using MyWallet.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services
{
    class ChartService : IChartService
    {
        private readonly MyWalletContext _db;

        public ChartService(MyWalletContext db)
        {
            _db = db;
        }

        public async Task<ChartModel> GetChart(DateTime? from = null, DateTime? to = null, Guid? categoryId = null)
        {
            var categories = await _db.Categories
                .Include(x => x.HistoryLines)
                .Include(x => x.Child).ThenInclude(x => x.HistoryLines).AsSplitQuery()
                .Where(x => categoryId.HasValue ? x.Id == categoryId.Value : !x.ParrentId.HasValue)
                .ToArrayAsync();

            if (!categoryId.HasValue)
            {
                var grouped = categories.GroupBy(x => (x.Label, x.Color), x => x.HistoryLines.Where(IsInDateRange).Union(x.Child.SelectMany(c => c.HistoryLines).Where(IsInDateRange)).Sum(h => h.Amount));

                return new ChartModel(
                    "Все траты",
                    grouped.Select(x => x.Key.Label).ToArray(),
                    grouped.Select(x => x.Sum()).ToArray(),
                    grouped.Select(x => x.Key.Color).ToArray());
            }
            else
            {
                var category = categories.First();
                var other = category.HistoryLines.Where(IsInDateRange).Sum(x => x.Amount);
                var grouped = category.Child.GroupBy(x => (x.Label, x.Color), x => x.HistoryLines.Where(IsInDateRange).Sum(l => l.Amount));

                return new ChartModel(
                    category.Label,
                    grouped.Select(x => x.Key.Label).Union(new string[] { category.Label }).ToArray(),
                    grouped.Select(x => x.Sum()).Union(new decimal[] { other }).ToArray(),
                    grouped.Select(x => x.Key.Color).Union(new string[] { category.Color }).ToArray());
            }

            

            bool IsInDateRange(HistoryLineEntity line)
            {
                return (from.HasValue ? line.Date.Date >= from.Value.Date : true) &&
                (to.HasValue ? line.Date.Date <= to.Value.Date : true);
            }
        }
    }
}
