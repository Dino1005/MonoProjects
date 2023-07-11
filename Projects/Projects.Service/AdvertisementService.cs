using Projects.Model;
using System.Collections.Generic;
using System;
using Projects.Service.Common;
using System.Threading.Tasks;
using Projects.Repository.Common;
using Projects.Common;

namespace Projects.Service
{
    public class AdvertisementService : IAdvertisementService
    {
        private IAdvertisementRepository AdvertisementRepository { get; }

        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            AdvertisementRepository = advertisementRepository;
        }
        public async Task<PageList<Advertisement>> GetAllAsync(Sorting sorting, Paging paging, AdvertisementFilter filter)
        {
            return await AdvertisementRepository.GetAllAsync(sorting, paging, filter);
        }

        public async Task<Advertisement> GetByIdAsync(Guid id)
        {
            return await AdvertisementRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Advertisement advertisement)
        {
            return await AdvertisementRepository.AddAsync(advertisement);
        }

        public async Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            return await AdvertisementRepository.UpdateAsync(id, advertisement);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await AdvertisementRepository.DeleteAsync(id);
        }
    }
}