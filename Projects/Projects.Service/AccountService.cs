using Projects.Service.Common;
using Projects.Model;
using Projects.Repository;
using System;
using System.Collections.Generic;

namespace Projects.Service
{
    public class AccountService : IAccountService
    {
        public List<Account> GetAll()
        {
            AccountRepository accountRepository = new AccountRepository();
            return accountRepository.GetAll();
        }

        public Account GetById(Guid id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return accountRepository.GetById(id);
        }

        public List<Advertisement> GetAdvertisements(Guid id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return accountRepository.GetAdvertisements(id);
        }

        public int Add(Account account)
        {
            AccountRepository accountRepository = new AccountRepository();
            return accountRepository.Add(account);
        }

        public int Update(Guid id, Account account)
        {
            AccountRepository accountRepository = new AccountRepository();
            return accountRepository.Update(id, account);
        }

        public int Delete(Guid id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return accountRepository.Delete(id);
        }
    }
}
