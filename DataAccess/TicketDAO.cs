using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess
{
    public class TicketDAO
    {
        private static TicketDAO instance = null;
        public static readonly object instanceLock = new object();
        public static TicketDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TicketDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Ticket> GetTicketList()
        {
            var ticket = new List<Ticket>();
            try
            {
                using var context = new CinemaProject_v4Context();
                ticket = context.Tickets.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ticket;
        }

        public Ticket GetTicketByID(int ticketID)
        {
            Ticket ticket = null;
            try
            {
                using var context = new CinemaProject_v4Context();
                ticket = context.Tickets.SingleOrDefault(c => c.Id == ticketID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ticket;
        }

        public void AddNewTicket(Ticket ticket)
        {
            try
            {
                Ticket _ticket = GetTicketByID(ticket.Id);
                if (_ticket == null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Tickets.Add(ticket);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The ticket is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTicket(Ticket ticket)
        {
            try
            {
                Ticket _ticket = GetTicketByID(ticket.Id);
                if (_ticket != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Tickets.Update(ticket);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The ticket does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveTicket(int ticketID)
        {
            try
            {
                Ticket ticket = GetTicketByID(ticketID);
                if (ticket != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Tickets.Remove(ticket);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The ticket does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
