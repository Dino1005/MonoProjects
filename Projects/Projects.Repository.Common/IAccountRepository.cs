using Projects.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projects.Common;

namespace Projects.Repository.Common
{
    public interface IAccountRepository
    {
        Task<PageList<Account>> GetAllAsync(Sorting sorting, Paging paging, AccountFilter filter);
        Task<Account> GetByIdAsync(Guid id);
        Task<List<Advertisement>> GetAdvertisementsAsync(Guid id);
        Task<int> AddAsync(Account account);
        Task<int> UpdateAsync(Guid id, Account account);
        Task<int> DeleteAsync(Guid id);

    }
}
