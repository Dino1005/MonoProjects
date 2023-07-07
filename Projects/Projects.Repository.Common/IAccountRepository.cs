using Projects.Model;
using System;
using System.Collections.Generic;

namespace Projects.Repository.Common
{
    public interface IAccountRepository
    {
        List<Account> GetAll();
        Account GetById(Guid id);
        List<Advertisement> GetAdvertisements(Guid id);
        int Add(Account account);
        int Update(Guid id, Account account);
        int Delete(Guid id);

    }
}
