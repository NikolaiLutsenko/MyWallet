using MyWallet.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallet.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<Category> Add(string name, string color, Guid? parrent = null);

		Task Update(Guid id, string name, string color, Guid? parrent);

		Task Remove(Guid id);

		Task<IReadOnlyCollection<Category>> GetAll();
	}
}
