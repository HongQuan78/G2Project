using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public class AccountRepository: IAccountRepository
    {
        public void DeleteAccount(int ID) => AccountDAO.Instance.Remove(ID);

        public Account GetAccountByID(int ID) => AccountDAO.Instance.GetAccountByID(ID);

        public IEnumerable<Account> GetAccounts() => AccountDAO.Instance.GetAccountList();

        public void InsertAccount(Account account) => AccountDAO.Instance.AddNew(account);

        public void UpdateAccount(Account account) => AccountDAO.Instance.Update(account);
    }
}
