using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class HotelRepository
    {
        private readonly string connectionString;
        public HotelRepository()
        { }

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
            throw new NotImplementedException();
        }

        public HotelModel Update(HotelModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
