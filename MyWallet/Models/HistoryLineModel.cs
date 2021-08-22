using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class HistoryLineModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public CategoryViewModel Category { get; set; }
    }

    public record CategoryViewModel(string Name, string Color);
}
