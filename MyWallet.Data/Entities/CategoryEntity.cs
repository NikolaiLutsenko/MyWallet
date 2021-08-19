using System;
using System.Collections.Generic;

namespace MyWallet.Data.Entities
{
	public class CategoryEntity
	{
		public Guid Id { get; set; }

		public string Label { get; set; }

		public string Color { get; set; }

		public Guid? ParrentId { get; set; }

		public CategoryEntity Parrent { get; set; }

		public IEnumerable<CategoryEntity> Child { get; set; }

		public IEnumerable<HistoryLineEntity> HistoryLines { get; set; }
	}
}
