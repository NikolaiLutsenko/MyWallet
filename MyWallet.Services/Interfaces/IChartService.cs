using MyWallet.Services.Dtos;
using System;
using System.Threading.Tasks;

namespace MyWallet.Services.Interfaces
{
    public interface IChartService
    {
        Task<ChartModel> GetChart(DateTime? from = null, DateTime? to = null, Guid? categoryId = null);
    }
}
