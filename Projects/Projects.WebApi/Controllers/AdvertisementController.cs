using Mono.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Security.Principal;
using System.Text;

namespace Mono.WebApi.Controllers
{
    public class AdvertisementController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllAdvertisements()
        {
            List<AdvertisementView> advertisements = new List<AdvertisementView>();
            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "SELECT * FROM Advertisement";
            command.Connection = connection;

            using (connection)
            {
                try
                {
                    connection.Open();

                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Guid id = (Guid)reader["Id"];
                            string title = (string)reader["Title"];
                            DateTime uploadDate = (DateTime)reader["UploadDate"];
                            advertisements.Add(new AdvertisementView(id, title, uploadDate));
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (advertisements.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, advertisements);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No advertisements found.");
        }

        [HttpGet]
        public HttpResponseMessage GetById(Guid id)
        {
            AdvertisementView advertisement = GetAdvertisementById(id);
            if (advertisement == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Advertisement with that ID was not found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, advertisement);
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No field can be empty!");
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "INSERT INTO Advertisement (Id, Title, UploadDate, CategoryId, PriorityId, AccountId) VALUES (@Id, @Title, @UploadDate, @CategoryId, @PriorityId, @AccountId)";
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", advertisement.Id);
            command.Parameters.AddWithValue("@Title", advertisement.Title);
            command.Parameters.AddWithValue("@UploadDate", advertisement.UploadDate);
            command.Parameters.AddWithValue("@CategoryId", advertisement.CategoryId);
            command.Parameters.AddWithValue("@PriorityId", advertisement.PriorityId);
            command.Parameters.AddWithValue("@AccountId", advertisement.AccountId);

            using (connection)
            {
                try
                {
                    connection.Open();

                    affectedRows = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

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
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No field can be empty!");
            }

            AdvertisementView advertisementToUpdate = GetAdvertisementById(id);
            if (advertisementToUpdate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Advertisement with that ID was not found!");
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", id);

            StringBuilder queryBuilder = new StringBuilder("UPDATE Advertisement SET ");

            if (!string.IsNullOrEmpty(advertisement.Title))
            {
                queryBuilder.Append("Title = @Title, ");
                command.Parameters.AddWithValue("@Title", advertisement.Title);
            }

            if (advertisement.CategoryId != null)
            {
                queryBuilder.Append("CategoryId = @CategoryId, ");
                command.Parameters.AddWithValue("@CategoryId", advertisement.CategoryId);
            }

            if (advertisement.PriorityId != null)
            {
                queryBuilder.Append("PriorityId = @PriorityId, ");
                command.Parameters.AddWithValue("@PriorityId", advertisement.PriorityId);
            }

            if (queryBuilder.Length > "UPDATE Advertisement SET ".Length)
            {
                queryBuilder.Length -= 2;
                queryBuilder.Append(" WHERE Id = @Id");
                command.CommandText = queryBuilder.ToString();

                using (connection)
                {
                    try
                    {
                        connection.Open();

                        affectedRows = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was updated. Affected rows: {affectedRows}");
                }
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

            AdvertisementView advertisement = GetAdvertisementById(id);
            if (advertisement == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Advertisement with that ID was not found!");
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "DELETE FROM Advertisement WHERE Id=@Id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", id);

            using (connection)
            {
                try
                {
                    connection.Open();

                    affectedRows = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Advertisement was deleted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Advertisement was not deleted!");
        }

        private AdvertisementView GetAdvertisementById(Guid id)
        {
            AdvertisementView advertisement = null;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "SELECT * FROM Advertisement WHERE Id=@Id";
            command.Parameters.AddWithValue("@Id", id);
            command.Connection = connection;

            using (connection)
            {
                try
                {
                    connection.Open();

                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string title = (string)reader["Title"];
                            DateTime uploadDate = (DateTime)reader["UploadDate"];
                            advertisement = new AdvertisementView(id, title, uploadDate);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return advertisement;
        }
    }
}