using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using Mono.WebApi.Models;
using Npgsql;
using System.Text;

namespace Mono.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllAccounts()
        {
            List<AccountView> accounts = new List<AccountView>();
            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "SELECT * FROM Account";
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
                            string firstName = (string)reader["FirstName"];
                            string lastName = (string)reader["LastName"];
                            accounts.Add(new AccountView(id, firstName, lastName));
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (accounts.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, accounts);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No accounts found.");
        }

        [HttpGet]
        public HttpResponseMessage GetById(Guid id)
        {
            AccountView account = GetAccountById(id);
            if (account == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Account with that ID was not found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, account);
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
                
            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "INSERT INTO Account (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";
            command.Connection = connection;
            Guid Id = Guid.NewGuid();
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@FirstName", account.FirstName);
            command.Parameters.AddWithValue("@LastName", account.LastName);

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

            AccountView accountToUpdate = GetAccountById(id);
            if(accountToUpdate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Account with that ID was not found!");
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", id);
            StringBuilder queryBuilder = new StringBuilder("UPDATE Account SET ");

            if (!string.IsNullOrEmpty(account.FirstName))
            {
                queryBuilder.Append("FirstName = @FirstName, ");
                command.Parameters.AddWithValue("@FirstName", account.FirstName);
            }

            if (!string.IsNullOrEmpty(account.LastName))
            {
                queryBuilder.Append("LastName = @LastName, ");
                command.Parameters.AddWithValue("@LastName", account.LastName);
            }

            if (queryBuilder.Length > "UPDATE Account SET ".Length)
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
                    return Request.CreateResponse(HttpStatusCode.OK, $"Account was updated. Affected rows: {affectedRows}");
                }
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

            AccountView account = GetAccountById(id);
            if (account == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Account with that ID was not found!");
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "DELETE FROM Account WHERE Id=@Id";
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
                return Request.CreateResponse(HttpStatusCode.OK, $"Account was deleted. Affected rows: {affectedRows}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account was not deleted!");
        }

        [Route("api/account/ads")]
        [HttpGet]
        public HttpResponseMessage GetAdvertisementsByAccount(Guid id)
        {
            AccountView account = GetAccountById(id);
            if (account == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Account with that ID was not found!");
            }

            List<AdvertisementView> advertisements = new List<AdvertisementView>();
            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "SELECT Id, Title, UploadDate FROM Advertisement WHERE AccountId=@Id";
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
                            Guid Id = (Guid)reader["Id"];
                            string title = (string)reader["Title"];
                            DateTime uploadDate = (DateTime)reader["UploadDate"];
                            advertisements.Add(new AdvertisementView(Id, title, uploadDate));
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

        private AccountView GetAccountById(Guid id)
        {
            AccountView account = null;

            NpgsqlConnection connection = new NpgsqlConnection(WebApiConfig.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "SELECT * FROM Account WHERE Id=@Id";
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
                            string firstName = (string)reader["FirstName"];
                            string lastName = (string)reader["LastName"];
                            account = new AccountView(id, firstName, lastName);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return account;
        }
    }
}