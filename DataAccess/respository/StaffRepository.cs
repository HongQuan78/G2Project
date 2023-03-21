using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public class StaffRepository: IStaffRepository
    {
        public staff GetStaffByID(int staffId) => StaffDAO.Instance.GetStaffByID(staffId);
        public IEnumerable<staff> GetStaffs() => StaffDAO.Instance.GetStaffList();
        public void InsertStaff(staff staff) => StaffDAO.Instance.AddNewStaff(staff);
        public void DeleteStaff(int staffId) => StaffDAO.Instance.RemoveStaff(staffId);
        public void UpdateStaff(staff staff) => StaffDAO.Instance.UpdateStaff(staff);

    }
}
