using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public class BillRepository: IBillRepository
    {
        public Bill GetBillByID(int billID) => BillDAO.Instance.GetBillByID(billID);
        public IEnumerable<Bill> GetBills() => BillDAO.Instance.GetBills();
        public void InsertBill(Bill bill) => BillDAO.Instance.AddNewBill(bill);
        public void UpdateBill(Bill bill) => BillDAO.Instance.UpdateBill(bill);
        public void DeleteBill(int billId) => BillDAO.Instance.RemoveBill(billId);
    }
}
