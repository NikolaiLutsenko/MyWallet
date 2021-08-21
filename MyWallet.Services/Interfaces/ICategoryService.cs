using MyWallet.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallet.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> Add(string userId, string name, string color, Guid? parrent = null);

        Task Update(string userId, Guid id, string name, string color, Guid? parrent);

        Task Remove(string userId, Guid id);

        Task<IReadOnlyCollection<Category>> GetAll(string userId);
    }
}
