using Projects.Model;
using System;
using System.Collections.Generic;

namespace Projects.Repository.Common
{
    public interface IAdvertisementRepository
    {
        List<Advertisement> GetAll();
        Advertisement GetById(Guid id);
        int Add(Advertisement account);
        int Update(Guid id, Advertisement account);
        int Delete(Guid id);
    }
}
