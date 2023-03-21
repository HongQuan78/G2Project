using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CinemaObject
{
    public partial class FastFood
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Size cannot null")]
        [RegularExpression("[MLS]", ErrorMessage = "Enter size as: S, M, L")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Price cannot null")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Image cannot null")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Quantity cannot null")]
        public int Quantity { get; set; }
    }
}
