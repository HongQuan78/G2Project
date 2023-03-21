using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CinemaObject
{
    public partial class staff
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Staff Name cannot be null!")]
        [StringLength(50, ErrorMessage = "Lenght of name must be from 5 to 50!", MinimumLength = 5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number cannot be null!")]
        public string PhoneNum { get; set; }
        [Required(ErrorMessage = "Address cannot be null!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Image cannot be null!")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Status cannot be null!")]
        public int Status { get; set; }
    }
}
