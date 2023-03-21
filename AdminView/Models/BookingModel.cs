using CinemaObject;
using System.Collections.Generic;
using System;

namespace AdminView.Models
{
    public class BookingModel
    {
        public DateTime Bookingdate { get; set; }
        public double Total { get; set; }
        public Movie Movie { get; set; }
        public string Seat_num { get; set; }
        public Bill Bill { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<FastFood> Food { get; set; }
        public IEnumerable<FastFood> Drink { get; set; }

    }
}
