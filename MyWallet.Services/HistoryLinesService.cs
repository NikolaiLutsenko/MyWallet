using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyWallet.Data;
using MyWallet.Data.Entities;
using MyWallet.Data.Enums;
using MyWallet.Models;
using MyWallet.Services.Dtos;
using MyWallet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyWallet.Services
{
    class HistoryLinesService : IHistoryLinesService
    {
        private readonly MyWalletContext _db;
        private readonly IMapper _mapper;

        public HistoryLinesService(MyWalletContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddCredit(string name, decimal amount, Guid categoryId)
        {
            _db.HistoryLines.Add(new HistoryLineEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Amount = amount,
                CategoryId = categoryId,
                Type = OperationType.Credit,
                Date = DateTime.Now.Date
            });
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<HistoryLine>> GetAll(string userId, DateRange dateRange = default, Guid? categoryId = null)
        {
            var lines = await _db.HistoryLines
                .Include(x => x.Category)
                .OrderByDescending(x => x.Date)
                .Where(x => x.Category.IdentityUserId == userId)
                .Where(DateRangePredicate())
                .Where(CategoryPredicate())
                .ToArrayAsync();

            return _mapper.Map<HistoryLine[]>(lines);

            Expression<Func<HistoryLineEntity, bool>> DateRangePredicate()
            {
                return (x) => dateRange.HasFrom ? x.Date.Date >= dateRange.From : false
                 || !dateRange.HasTo || x.Date.Date <= dateRange.To;
            }

            Expression<Func<HistoryLineEntity, bool>> CategoryPredicate()
            {
                return (x) => !categoryId.HasValue || x.CategoryId == categoryId.Value || x.Category.Parrent.Id == categoryId.Value;
            }
        }

        public async Task Remove(Guid id)
        {
            var item = await _db.HistoryLines.FindAsync(id);
            if (item == null) return;

            _db.HistoryLines.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
