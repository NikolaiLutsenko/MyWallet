using MyWallet.Data.ValueObjects;
using MyWallet.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallet.Services.Interfaces
{
    public interface IHistoryLinesService
    {
        Task AddCredit(string name, decimal amount, Guid categoryId);

        Task Remove(Guid id);

        Task<IReadOnlyCollection<HistoryLine>> GetAll(string userId, DateRange dateRange, Guid? categoryId = null);
    }
}
