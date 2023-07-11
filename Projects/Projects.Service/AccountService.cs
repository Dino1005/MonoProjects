using Projects.Service.Common;
using Projects.Model;
using Projects.Repository.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; }

        public AccountService(IAccountRepository accountRepository)
        {
            AccountRepository = accountRepository;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await AccountRepository.GetAllAsync();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await AccountRepository.GetByIdAsync(id);
        }

        public async Task<List<Advertisement>> GetAdvertisementsAsync(Guid id)
        {
            return await AccountRepository.GetAdvertisementsAsync(id);
        }

        public async Task<int> AddAsync(Account account)
        {
            return await AccountRepository.AddAsync(account);
        }

        public async Task<int> UpdateAsync(Guid id, Account account)
        {
            return await AccountRepository.UpdateAsync(id, account);
        }

        public async Task<int> DeleteAsync(Guid id)
        {   
            return await AccountRepository.DeleteAsync(id);
        }
    }
}
