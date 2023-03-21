using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public interface IStaffRepository
    {
        IEnumerable<staff> GetStaffs();
        staff GetStaffByID(int staffId);
        void DeleteStaff(int staffId);
        void InsertStaff(staff staff);
        void UpdateStaff(staff staff);
    }
}
