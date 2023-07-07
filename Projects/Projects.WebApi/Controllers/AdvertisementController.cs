using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Projects.Model;
using Projects.WebApi.Models;
using Projects.Service;

namespace Mono.WebApi.Controllers
{
    public class AdvertisementController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllAdvertisements()
        {
            AdvertisementService advertisementService = new AdvertisementService();
            List<Advertisement> advertisements = advertisementService.GetAll();
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
        public HttpResponseMessage GetById(Guid id)
        {
            AdvertisementService advertisementService = new AdvertisementService();
            Advertisement advertisement = advertisementService.GetById(id);
            if (advertisement == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Advertisement with that ID was not found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, new AdvertisementView(advertisement.Title, advertisement.UploadDate));
        }

        [HttpPost]
        public HttpResponseMessage InsertAdvertisement([FromBody] AdvertisementCreate advertisement)
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

            AdvertisementService advertisementService = new AdvertisementService();
            int affectedRows = advertisementService.Add(advertisementToInsert);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was inserted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement was not inserted!");
        }

        [HttpPut]
        public HttpResponseMessage UpdateAdvertisement(Guid id, [FromBody] AdvertisementUpdate advertisement)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is null!");
            }
            if (advertisement == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement is null!");
            }

            Advertisement advertisementToUpdate = new Advertisement(id, advertisement.Title, null, advertisement.CategoryId, advertisement.PriorityId, null);
            AdvertisementService advertisementService = new AdvertisementService();
            int affectedRows = advertisementService.Update(id, advertisementToUpdate);
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
        public HttpResponseMessage DeleteAdvertisement(Guid id)
        {
            if (id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is null!");
            }

            AdvertisementService advertisementService = new AdvertisementService();
            int affectedRows = advertisementService.Delete(id);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was deleted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement was not deleted!");
        }
    }
}