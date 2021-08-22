using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class HistoryLinesModel
    {
        [Display(Name = "C:")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Display(Name = "До:")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        [Display(Name = "Категория")]
        public Guid? CategoryId { get; set; }

        public HistoryLineModel[] Lines { get; set; }
    }
}
