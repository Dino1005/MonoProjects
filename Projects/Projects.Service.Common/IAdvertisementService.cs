using Projects.Common;
using Projects.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.Service.Common
{
    public interface IAdvertisementService
    {
        Task<PageList<Advertisement>> GetAllAsync(Sorting sorting, Paging paging, AdvertisementFilter filter);
        Task<Advertisement> GetByIdAsync(Guid id);
        Task<int> AddAsync(Advertisement account);
        Task<int> UpdateAsync(Guid id, Advertisement account);
        Task<int> DeleteAsync(Guid id);
    }
}
