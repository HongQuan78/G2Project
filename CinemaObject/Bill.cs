using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaObject
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int IdMovie { get; set; }
        public string TicketType { get; set; }
        public string SeatNum { get; set; }
        public int FastFood { get; set; }
        public int QuantityFastfood { get; set; }
        public int Drink { get; set; }
        public int QuantityDrink { get; set; }
        public double Total { get; set; }
        public int TicketId { get; set; }
        public DateTime BookingDate { get; set; }

        public virtual Movie IdMovieNavigation { get; set; }
    }
}
