using CinemaObject;
using System.Collections.Generic;

namespace AdminView.Models
{
    public class ViewCreateTicketModel
    {
        public Ticket Ticket { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

    }
}
