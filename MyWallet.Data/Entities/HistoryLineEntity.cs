using MyWallet.Data.Enums;
using System;

namespace MyWallet.Data.Entities
{
	public class HistoryLineEntity
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
