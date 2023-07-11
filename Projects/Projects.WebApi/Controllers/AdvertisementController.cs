using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Projects.Model;
using Projects.WebApi.Models;
using Projects.Service;
using System.Threading.Tasks;
using Projects.Service.Common;

namespace Mono.WebApi.Controllers
{
    public class AdvertisementController : ApiController
    {
        private IAdvertisementService AdvertisementService { get; }

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            AdvertisementService = advertisementService;
        }
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAdvertisements()
        {
            List<Advertisement> advertisements = await AdvertisementService.GetAllAsync();
            if (advertisements.Count <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No advertisements found.");
            }

            List<AdvertisementView> advertisementViews = new List<AdvertisementView>();
            foreach (var advertisement in advertisements)
            {
                advertisementViews.Add(new AdvertisementView(advertisement.Title, advertisement.UploadDate));
            }
            return Request.CreateResponse(HttpStatusCode.OK, advertisementViews);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            Advertisement advertisement = await AdvertisementService.GetByIdAsync(id);
            if (advertisement == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Advertisement with that ID was not found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, new AdvertisementView(advertisement.Title, advertisement.UploadDate));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> InsertAdvertisement([FromBody] AdvertisementCreate advertisement)
        {
            if (advertisement == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement is null!");
            }
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "All fields must be entered!");
            }
            Advertisement advertisementToInsert = new Advertisement(Guid.NewGuid(), advertisement.Title, advertisement.UploadDate, advertisement.CategoryId, advertisement.PriorityId, advertisement.AccountId);

            int affectedRows = await AdvertisementService.AddAsync(advertisementToInsert);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was inserted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement was not inserted!");
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAdvertisement(Guid id, [FromBody] AdvertisementUpdate advertisement)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is null!");
            }
            if (advertisement == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement is null!");
            }

            Advertisement advertisementById = await AdvertisementService.GetByIdAsync(id);
            if (advertisementById == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement with that id was not found!");
            }

            Advertisement advertisementToUpdate = new Advertisement(id, advertisement.Title, null, advertisement.CategoryId, advertisement.PriorityId, null);
            int affectedRows = await AdvertisementService.UpdateAsync(id, advertisementToUpdate);
            if (affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Advertisement with that ID was not found!");
            }
            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was updated. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement was not updated!");
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAdvertisement(Guid id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is null!");
            }

            int affectedRows = await AdvertisementService.DeleteAsync(id);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was deleted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement was not deleted!");
        }
    }
}