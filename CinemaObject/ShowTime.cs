using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaObject
{
    public partial class ShowTime
    {
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime ShowDate { get; set; }
        public double TicketPrice { get; set; }
        public int NumOfTicketSold { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
    }
}
