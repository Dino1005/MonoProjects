﻿using Npgsql;
using Projects.Model;
using Projects.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        public List<Advertisement> GetAll()
        {
            List<Advertisement> advertisements = new List<Advertisement>();
            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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

        public Advertisement GetById(Guid id)
        {
            Advertisement advertisement = GetAdvertisementById(id);
            return advertisement;
        }

        public int Add(Advertisement advertisement)
        {
            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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

            return affectedRows;
        }

        public int Update(Guid id, Advertisement advertisement)
        {
            Advertisement advertisementToUpdate = GetAdvertisementById(id);
            if (advertisementToUpdate == null)
            {
                return 0;
            }

            int affectedRows = 0;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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
            }
            return affectedRows;
        }

        public int Delete(Guid id)
        {
            Advertisement advertisementToDelete = GetAdvertisementById(id);
            if (advertisementToDelete == null)
            {
                return 0;
            }

            int affectedRows;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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

            return affectedRows;
        }

        private Advertisement GetAdvertisementById(Guid id)
        {
            Advertisement advertisement = null;

            NpgsqlConnection connection = new NpgsqlConnection(Database.connectionString);

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
                            Guid Id = (Guid)reader["Id"];
                            string title = (string)reader["Title"];
                            DateTime uploadDate = (DateTime)reader["UploadDate"];
                            Guid categoryId = (Guid)reader["CategoryId"];
                            Guid priorityId = (Guid)reader["PriorityId"];
                            Guid accountId = (Guid)reader["AccountId"];
                            advertisement = new Advertisement(Id, title, uploadDate, categoryId, priorityId, accountId);
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
