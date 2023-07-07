using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Projects.WebApi.Models;
using Projects.Service;
using Projects.Model;

namespace Projects.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllAccounts()
        {
            AccountService accountService = new AccountService();
            List<Account> accounts = accountService.GetAll();
            if(accounts.Count <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No accounts found.");
            }

            List<AccountView> accountViews = new List<AccountView>();
            foreach (var account in accounts)
            {
                accountViews.Add(new AccountView(account.FirstName, account.LastName));
            }
            return Request.CreateResponse(HttpStatusCode.OK, accountViews);
        }

        [HttpGet]
        public HttpResponseMessage GetById(Guid id)
        {
            AccountService accountService = new AccountService();
            Account account = accountService.GetById(id);
            if (account == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Account with that ID was not found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, new AccountView(account.FirstName, account.LastName));
        }

        [HttpPost]
        public HttpResponseMessage InsertAccount([FromBody] AccountCreate account)
        {
            if(account == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account is null!");
            }
            if (!ModelState.IsValid) 
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "First name and last name can't be empty!");
            }
            Account accountToInsert = new Account(Guid.NewGuid(), account.FirstName, account.LastName);
                
            AccountService accountService = new AccountService();
            int affectedRows = accountService.Add(accountToInsert);

            if(affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Account was inserted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account was not inserted!");
        }

        [HttpPut]
        public HttpResponseMessage UpdateAccount(Guid id, [FromBody] AccountUpdate account)
        {
            if(id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is null!");
            }
            if (account == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account is null!");
            }

            Account accountToUpdate = new Account(id, account.FirstName, account.LastName);
            AccountService accountService = new AccountService();
            int affectedRows = accountService.Update(id, accountToUpdate);
            if (affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Account with that ID was not found!");
            }
            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Account was updated. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account was not updated!");
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAccount(Guid id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is null!");
            }

            AccountService accountService = new AccountService();
            int affectedRows = accountService.Delete(id);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Account was deleted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account was not deleted!");
        }

        [Route("api/account/ads")]
        [HttpGet]
        public HttpResponseMessage GetAdvertisementsByAccount(Guid id)
        {
            AccountService accountService = new AccountService();
            List<Advertisement> advertisements = accountService.GetAdvertisements(id);

            if (advertisements.Any())
            {
                List<AdvertisementView> advertisementViews = new List<AdvertisementView>();
                foreach (var advertisement in advertisements)
                {
                    advertisementViews.Add(new AdvertisementView(advertisement.Title, advertisement.UploadDate));
                }
                return Request.CreateResponse(HttpStatusCode.OK, advertisements);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No advertisements found.");
        }
    }
}