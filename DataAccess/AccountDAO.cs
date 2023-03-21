using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLook = new object();
        public static AccountDAO Instance
        {
            get {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                }

                return instance;
            }
        }

        public IEnumerable<Account> GetAccountList()
        {
            var accounts = new List<Account>();
            try
            {
                using var context = new CinemaProject_v4Context();
                accounts = context.Accounts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return accounts;
        }

        public Account GetAccountByID(int ID)
        {
            Account account = null;
            try
            {
                using var context = new CinemaProject_v4Context();
                account = context.Accounts.SingleOrDefault(a => a.Id == ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public void AddNew(Account account)
        {
            try
            {
                Account _account = GetAccountByID(account.Id);
                if (_account == null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Accounts.Add(account);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Account is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Update(Account account)
        {
            try
            {
                Account _account = GetAccountByID(account.Id);
                if (_account != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Accounts.Update(account);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Account does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int ID)
        {
            try
            {
                Account account = GetAccountByID(ID);

                if (account != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Accounts.Remove(account);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Account does not already exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
