using Projects.Model;
using System;
using System.Collections.Generic;

namespace Projects.Service.Common
{
    public interface IAccountService
    {
        List<Account> GetAll();
        Account GetById(Guid id);
        List<Advertisement> GetAdvertisements(Guid id);
        int Add(Account account);
        int Update(Guid id, Account account);
        int Delete(Guid id);
    }
}
