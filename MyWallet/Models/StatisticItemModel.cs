using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class StatisticItemModel
    {
        [Display(Name = "Категория")]
        public string Label { get; set; }

        [Display(Name = "Предыдущая сумма")]
        public decimal PrevAmount { get; set; }

        [Display(Name = "Текущая сумма")]
        public decimal CurrentAmount { get; set; }

        [Display(Name = "Процент")]
        public decimal Percent { get; set; }
    }
}
