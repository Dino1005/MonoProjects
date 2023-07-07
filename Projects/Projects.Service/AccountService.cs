using Projects.Service.Common;
using Projects.Model;
using Projects.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.Service
{
    public class AccountService : IAccountService
    {
        public async Task<List<Account>> GetAllAsync()
        {
            AccountRepository accountRepository = new AccountRepository();
            return await accountRepository.GetAllAsync();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return await accountRepository.GetByIdAsync(id);
        }

        public async Task<List<Advertisement>> GetAdvertisementsAsync(Guid id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return await accountRepository.GetAdvertisementsAsync(id);
        }

        public async Task<int> AddAsync(Account account)
        {
            AccountRepository accountRepository = new AccountRepository();
            return await accountRepository.AddAsync(account);
        }

        public async Task<int> UpdateAsync(Guid id, Account account)
        {
            AccountRepository accountRepository = new AccountRepository();
            return await accountRepository.UpdateAsync(id, account);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return await accountRepository.DeleteAsync(id);
        }
    }
}
