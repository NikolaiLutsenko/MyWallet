using System;
using System.Collections.Generic;

namespace MyWallet.Services.Dtos
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Color { get; set; }
		public Category Parrent { get; set; }
		public IReadOnlyCollection<Category> Child { get; set; }
	}
}
