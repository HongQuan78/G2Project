using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess
{
    public class BillDAO
    {
        private static BillDAO instance = null;
        public static readonly object instanceLock = new object();
        public static BillDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BillDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Bill> GetBills()
        {
            var bill = new List<Bill>();
            try
            {
                using var context = new CinemaProject_v4Context();
                bill = context.Bills.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bill;
        }

        public Bill GetBillByID(int billID)
        {
            Bill bill = null;
            try
            {
                using var context = new CinemaProject_v4Context();
                bill = context.Bills.SingleOrDefault(c => c.Id == billID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bill;
        }

        public void AddNewBill(Bill bill)
        {
            try
            {
                TicketDAO ticketDAO = new TicketDAO();
                FastFoodDAO fastFoodDAO = new FastFoodDAO();
                Bill _bill = GetBillByID(bill.Id);
                if (_bill == null)
                {
                    FastFood Food = fastFoodDAO.GetFastFoodByID(bill.FastFood);
                    FastFood Drink = fastFoodDAO.GetFastFoodByID(bill.Drink);
                    Ticket ticket = ticketDAO.GetTicketByID(bill.TicketId);
                    bill.Total = 0;

                    bill.Total = ticket.Price + Food.Price * bill.QuantityFastfood + Drink.Price * bill.QuantityDrink;

                    using var context = new CinemaProject_v4Context();
                    context.Bills.Add(bill);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The fast food is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBill(Bill bill)
        {
            try
            {
                Bill _bill = GetBillByID(bill.Id);
                if (_bill != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Bills.Update(bill);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The bill does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveBill(int billID)
        {
            try
            {
                Bill bill = GetBillByID(billID);
                if (bill != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Bills.Remove(bill);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The bill does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
