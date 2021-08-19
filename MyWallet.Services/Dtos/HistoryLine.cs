using System;

namespace MyWallet.Services.Dtos
{
    public class HistoryLine
    {
        public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Amount { get; set; }

		public string Type { get; set; }

		public DateTime Date { get; set; }

		public Category Category { get; set; }
	}
}
