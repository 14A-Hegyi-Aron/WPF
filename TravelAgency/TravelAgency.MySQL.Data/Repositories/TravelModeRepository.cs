using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class TravelModeRepository
    {
        private readonly string connectionString;
        public TravelModeRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["travels"].ConnectionString;
        }

        public TravelModeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<TravelModeModel> GetAll()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "select id, name from travelModes";
                var travelModes = new List<TravelModeModel>();
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        travelModes.Add
                            (
                            new TravelModeModel()
                            {
                                Id = (int)reader["id"],
                                Name = reader["name"].ToString()
                            }
                        );
                    }
                }
                return travelModes;
            }
        }

        public TravelModeModel Insert(TravelModeModel model)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "insert into travelModes (name) values (@name)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("name", model.Name);
                    cmd.ExecuteNonQuery();
                    return new TravelModeModel()
                    {
                        Id = (int)cmd.LastInsertedId,
                        Name = model.Name
                    };
                }
            }
        }

        public TravelModeModel Update(TravelModeModel model)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "update travelModes set name = @name where id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("name", model.Name);
                    cmd.Parameters.AddWithValue("id", model.Id);
                    cmd.ExecuteNonQuery();
                    return new TravelModeModel()
                    {
                        Id = model.Id,
                        Name = model.Name
                    };
                    // return model; is also ok
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "delete from travelModes where id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
