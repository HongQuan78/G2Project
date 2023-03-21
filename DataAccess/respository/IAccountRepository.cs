using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccountByID(int ID);
        void InsertAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(int ID);
    }
}
