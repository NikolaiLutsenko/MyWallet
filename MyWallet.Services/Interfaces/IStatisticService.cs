using MyWallet.Models;
using MyWallet.Services.Dtos;
using System;
using System.Threading.Tasks;

namespace MyWallet.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<StatisticItem[]> GetStatistic(string userId, DateRange prevRange, DateRange currentRange, Guid? categoryId = null);
    }
}
