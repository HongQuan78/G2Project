using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public class TicketRepository: ITicketRepository
    {
        public Ticket GetTicketByID(int ticketID) => TicketDAO.Instance.GetTicketByID(ticketID);
        public IEnumerable<Ticket> GetTickets() => TicketDAO.Instance.GetTicketList();
        public void InsertTicket(Ticket ticket) => TicketDAO.Instance.AddNewTicket(ticket);
        public void DeleteTicket(int ticketID) => TicketDAO.Instance.RemoveTicket(ticketID);
        public void UpdateTicket(Ticket ticket) => TicketDAO.Instance.UpdateTicket(ticket);
    }
}
