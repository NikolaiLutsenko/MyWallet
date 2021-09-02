using MyWallet.Data.ValueObjects;
using MyWallet.Services.Dtos;
using System;
using System.Threading.Tasks;

namespace MyWallet.Services.Interfaces
{
    public interface IChartService
    {
        Task<ChartModel> GetChart(string userId, DateRange dateRange, Guid? categoryId = null);
    }
}
