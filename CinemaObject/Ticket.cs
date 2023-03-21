using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CinemaObject
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int IdMovie { get; set; }
        [Required(ErrorMessage = "Ticket type cannot null")]
        public string TicketType { get; set; }

        [Required(ErrorMessage = "Price type cannot null")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity type cannot null")]
        public int Quantity { get; set; }

        public virtual Movie IdMovieNavigation { get; set; }
    }
}
