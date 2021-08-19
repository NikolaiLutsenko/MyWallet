using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models
{
    public class CreateCategory
    {
        [HiddenInput]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Цвет обязателен")]
        public string Color { get; set; }

        public Guid? ParrentId { get; set; }

        public SelectList Categories { get; set; }
    }
}
