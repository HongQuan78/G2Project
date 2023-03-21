using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public interface IBillRepository
    {
        IEnumerable<Bill> GetBills();
        Bill GetBillByID(int billID);
        void InsertBill(Bill bill);
        void UpdateBill(Bill bill);
        void DeleteBill(int billID);
    }
}
