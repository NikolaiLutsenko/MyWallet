using MyWallet.Data.ValueObjects;
using MyWallet.Services.Dtos;
using MyWallet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallet.Services
{
    internal class StatisticService : IStatisticService
    {
        private readonly IChartService _chartService;

        public StatisticService(IChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task<StatisticItem[]> GetStatistic(string userId, DateRange prevRange, DateRange currentRange, Guid? categoryId = null)
        {
            var currentChart = await _chartService.GetChart(userId, currentRange, categoryId);
            var prevChart = await _chartService.GetChart(userId, prevRange, categoryId);

            var currentStatistic = ToDictionary(currentChart);
            var prevStatistic = ToDictionary(prevChart);

            var statisticItems = new List<StatisticItem>();
            foreach (var prevItem in prevStatistic)
            {
                var prevAmount = prevItem.Value;
                var currentAmount = currentStatistic[prevItem.Key];

                var percent = prevAmount == 0 ? 0 : Math.Round(currentAmount / prevAmount * 100, 2);

                statisticItems.Add(new StatisticItem(prevItem.Key, prevAmount, currentAmount, percent));
            }

            return statisticItems.ToArray();

            static IDictionary<string, decimal> ToDictionary(ChartModel chart)
            {
                var statistic = new Dictionary<string, decimal>();
                for (int i = 0; i < chart.Labels.Length; i++)
                    statistic[chart.Labels[i]] = chart.Amounts[i];

                return statistic;
            }
        }
    }
}
