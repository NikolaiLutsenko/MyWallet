using MyWallet.Data.Enums;
using MyWallet.Data.Interfaces;
using System;

namespace MyWallet.Data.Entities
{
	public class HistoryLineEntity: HasCreatedAt, HasCategory
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Amount { get; set; }

		public OperationType Type { get; set; }

		public DateTime Date { get; set; }

		public Guid CategoryId { get; set; }

		public CategoryEntity Category { get; set; }
	}
}
