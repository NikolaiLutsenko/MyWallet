using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class ChartModel
    {
        public string[] Labels { get; set; }

        public decimal[] Amounts { get; set; }

        public string[] Colors { get; set; }
        public string DataSetLabel { get; set; }

        [Display(Name = "C:")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Display(Name = "До:")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
    }
}
