﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWallet.Data;
using MyWallet.Data.Entities;
using MyWallet.Services.Dtos;
using MyWallet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallet.Services
{
	class CategoryService : ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly MyWalletContext _db;
		private readonly ILogger<CategoryService> _logger;

		public CategoryService(IMapper mapper, MyWalletContext db, ILogger<CategoryService> logger)
		{
			_mapper = mapper;
			_db = db;
			_logger = logger;
		}

		public async Task<Category> Add(string name, string color, Guid? parrent = null)
		{
			var id = Guid.NewGuid();
			_db.Categories.Add(new CategoryEntity
			{
				Id = id,
				Color = color,
				Label = name,
				ParrentId = parrent
			});

			await _db.SaveChangesAsync();

			var category = await _db.Categories
				.Include(x => x.Parrent)
				.Include(x => x.Child)
				.SingleOrDefaultAsync(x => x.Id == id);

			return _mapper.Map<Category>(category);
		}

		public async Task<IReadOnlyCollection<Category>> GetAll()
		{
			var categories = await _db.Categories
				.Include(x => x.Parrent)
				.Include(x => x.Child)
				.ToArrayAsync();

			return _mapper.Map<Category[]>(categories);
		}

		public async Task Remove(Guid id)
		{
			var category = await _db.Categories.FindAsync(id);

			if (category == null) return;

			_db.Categories.Remove(category);

			await _db.SaveChangesAsync();
		}

		public async Task Update(Guid id, string name, string color, Guid? parrent)
		{
			var category = await _db.Categories.FindAsync(id);

			if (category == null) return;

			category.Label = name;
			category.ParrentId = parrent;
			category.Color = color;

			await _db.SaveChangesAsync();
		}
	}
}
