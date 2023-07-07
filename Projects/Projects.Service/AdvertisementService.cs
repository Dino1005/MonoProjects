using Projects.Model;
using Projects.Repository;
using System.Collections.Generic;
using System;

namespace Projects.Service
{
    public class AdvertisementService
    {
        public List<Advertisement> GetAll()
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return advertisementRepository.GetAll();
        }

        public Advertisement GetById(Guid id)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return advertisementRepository.GetById(id);
        }

        public int Add(Advertisement advertisement)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return advertisementRepository.Add(advertisement);
        }

        public int Update(Guid id, Advertisement advertisement)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return advertisementRepository.Update(id, advertisement);
        }

        public int Delete(Guid id)
        {
            AdvertisementRepository advertisementRepository = new AdvertisementRepository();
            return advertisementRepository.Delete(id);
        }
    }
}