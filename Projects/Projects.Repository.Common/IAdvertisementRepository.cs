using Projects.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.Repository.Common
{
    public interface IAdvertisementRepository
    {
        Task<List<Advertisement>> GetAllAsync();
        Task<Advertisement> GetByIdAsync(Guid id);
        Task<int> AddAsync(Advertisement account);
        Task<int> UpdateAsync(Guid id, Advertisement account);
        Task<int> DeleteAsync(Guid id);
    }
}
