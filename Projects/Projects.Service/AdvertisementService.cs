using Projects.Model;
using Projects.Repository;
using System.Collections.Generic;
using System;
using Projects.Service.Common;
using System.Threading.Tasks;

namespace Projects.Service
{
    public class AdvertisementService : IAdvertisementService
    {
        public async Task<List<Advertisement>> GetAllAsync()
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return await advertisementRepository.GetAllAsync();
        }

        public async Task<Advertisement> GetByIdAsync(Guid id)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return await advertisementRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Advertisement advertisement)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return await advertisementRepository.AddAsync(advertisement);
        }

        public async Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return await advertisementRepository.UpdateAsync(id, advertisement);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return await advertisementRepository.DeleteAsync(id);
        }
    }
}