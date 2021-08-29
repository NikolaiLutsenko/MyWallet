using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class ChartModel
    {
        private readonly DateRange _dateRange;

        public ChartModel(DateRange dateRange)
        {
            _dateRange = dateRange;
        }

        public string[] Labels { get; set; }

        public decimal[] Amounts { get; set; }

        public string[] Colors { get; set; }
        public string DataSetLabel { get; set; }

        [Display(Name = "C:")]
        [DataType(DataType.Date)]
        public DateTime? From => _dateRange.From;

        [Display(Name = "До:")]
        [DataType(DataType.Date)]
        public DateTime? To => _dateRange.To;
    }
}
