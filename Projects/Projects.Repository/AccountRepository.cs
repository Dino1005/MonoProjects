using Projects.Model;
using Projects.Repository.Common;
using Npgsql;
using System.Text;
using System.Collections.Generic;
using System;

namespace Projects.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public List<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();
            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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
                            accounts.Add(new Account(id, firstName, lastName));
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return accounts;
        }

        public Account GetById(Guid id)
        {
            Account account = GetAccountById(id);
            return account;
        }

        public List<Advertisement> GetAdvertisements(Guid id)
        {
            List<Advertisement> advertisements = new List<Advertisement>();
            Account account = GetAccountById(id);
            if (account == null)
            {
                return advertisements;
            }

            
            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "SELECT Id, Title, UploadDate, CategoryId, PriorityId, AccountId FROM Advertisement WHERE AccountId=@Id";
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
                            Guid categoryId = (Guid)reader["CategoryId"];
                            Guid priorityId = (Guid)reader["PriorityId"];
                            Guid accountId = (Guid)reader["AccountId"];
                            advertisements.Add(new Advertisement(Id, title, uploadDate, categoryId, priorityId, accountId));
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return advertisements;
        }

        public int Add(Account account)
        {
            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = "INSERT INTO Account (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", account.Id);
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

            return affectedRows;
        }

        public int Update(Guid id, Account account)
        {
            Account accountToUpdate = GetAccountById(id);
            if(accountToUpdate == null)
            {
                return 0;
            }   

            int affectedRows = 0;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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
            }
            return affectedRows;
        }

        public int Delete(Guid id)
        {
            Account accountToDelete = GetAccountById(id);
            if (accountToDelete == null)
            {
                return 0;
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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

            return affectedRows;
        }

        private Account GetAccountById(Guid id)
        {
            Account account = null;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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
                            account = new Account(id, firstName, lastName);
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