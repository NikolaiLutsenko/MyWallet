using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyWallet.Data;
using MyWallet.Data.Entities;
using MyWallet.Data.Enums;
using MyWallet.Services.Dtos;
using MyWallet.Services.Interfaces;
using System;
using System.Collections.Generic;
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
                Date = DateTime.Now
            });
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<HistoryLine>> GetAll()
        {
            var lines = await _db.HistoryLines
                .Include(x => x.Category)
                .ToArrayAsync();

            return _mapper.Map<HistoryLine[]>(lines);
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
