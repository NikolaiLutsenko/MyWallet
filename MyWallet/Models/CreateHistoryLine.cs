using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class CreateHistoryLine
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Сумма")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Поле категория обязательно")]
        public Guid CategoryId { get; set; }

        public SelectList Categories { get; set; }
    }
}
