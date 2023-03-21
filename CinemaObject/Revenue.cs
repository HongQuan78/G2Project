using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaObject
{
    public partial class Revenue
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public DateTime PremiereDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfTicketSold { get; set; }
        public double Revenue1 { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
