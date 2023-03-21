using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess
{
    public class StaffDAO
    {
        private static StaffDAO instance = null;
        public static readonly object instanceLock = new object();
        public static StaffDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StaffDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<staff> GetStaffList()
        {
            var staffs = new List<staff>();
            try
            {
                using var context = new CinemaProject_v4Context();
                staffs = context.staff.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return staffs;
        }

        public staff GetStaffByID(int staffID)
        {
            staff staff = null;
            try
            {
                using var context = new CinemaProject_v4Context();
                staff = context.staff.SingleOrDefault(c => c.Id == staffID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return staff;
        }

        public void AddNewStaff(staff staff)
        {
            try
            {
                staff _staff = GetStaffByID(staff.Id);
                if (_staff == null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.staff.Add(staff);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Staff is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStaff(staff staff)
        {
            try
            {
                staff _staff = GetStaffByID(staff.Id);
                if (_staff != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.staff.Update(staff);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Staff does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveStaff(int staffID)
        {
            try
            {
                staff staff = GetStaffByID(staffID);
                if (staff != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.staff.Remove(staff);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Staff does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
