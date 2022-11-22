using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class HotelRepository
    {
        private readonly string connectionString;
        public HotelRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["travels"].ConnectionString;
        }

        public HotelRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<HotelModel> GetAll()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "select id, name, stars, address, webPageUrl, description from hotels";
                var hotels = new List<HotelModel>();
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hotels.Add
                            (
                            new HotelModel()
                            {
                                Id = (int)reader["id"],
                                Name = reader["name"].ToString(),
                                Stars = (int)reader["stars"],
                                // if stars could be null, then we should use:
                                //Stars = reader["stars"] == DBNull.Value ? null : (int?)reader["stars"],
                                Address = reader["address"].ToString(),
                                WebPageUrl = reader["webPageUrl"].ToString(),
                                Description = reader["description"].ToString()
                            }
                        );
                    }
                }
                return hotels;
            }
        }

        public HotelModel Insert(HotelModel model)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "insert into hotels (name, stars, address, webPageUrl, description) " +
                    "values (@name, @stars, @address, @webPageUrl, @description)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", model.Name);
                    cmd.Parameters.AddWithValue("@stars", model.Stars);
                    cmd.Parameters.AddWithValue("@address", model.Address);
                    cmd.Parameters.AddWithValue("@webPageUrl", model.WebPageUrl);
                    cmd.Parameters.AddWithValue("@description", model.Description);
                    cmd.ExecuteNonQuery();
                    model.Id = (int)cmd.LastInsertedId;
                    return model;
                }
            }
        }

        public HotelModel Update(HotelModel model)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "update hotels set name = @name, stars = @stars, address = @address, " +
                    "webPageUrl = @webPageUrl, description = @description where id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", model.Name);
                    cmd.Parameters.AddWithValue("@stars", model.Stars);
                    cmd.Parameters.AddWithValue("@address", model.Address);
                    cmd.Parameters.AddWithValue("@webPageUrl", model.WebPageUrl);
                    cmd.Parameters.AddWithValue("@description", model.Description);
                    cmd.Parameters.AddWithValue("@id", model.Id);
                    cmd.ExecuteNonQuery();
                    return model;
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "delete from hotels where id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
