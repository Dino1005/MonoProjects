using Projects.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.Repository.Common
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(Guid id);
        Task<List<Advertisement>> GetAdvertisementsAsync(Guid id);
        Task<int> AddAsync(Account account);
        Task<int> UpdateAsync(Guid id, Account account);
        Task<int> DeleteAsync(Guid id);

    }
}
