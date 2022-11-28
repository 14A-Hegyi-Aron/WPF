using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class OfferRepository
    {
        private readonly string connectionString;
        public OfferRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["travels"].ConnectionString;
        }

        private string SelectSql()
        {
            {
                return "select offers.id, " +
                    "offers.destination, " +
                    "offers.travelModeId as modeId, " +
                    "offers.startDate, " +
                    "offers.endDate, " +
                    "offers.hotelId, " +
                    "offers.price, " +
                    "offers.description, " +
                    "offers.maxParticipants, " +
                    "offers.photo, " +
                    "travelMode.Name as modeName, " +
                    "hotels.name as hotelName " +
                    "from offers" +
                    "left join travelModes on offers.travelModeId = travelModes.id " +
                    "left join hotels on offers.hotelId = hotels.id";
            }
        }

        public IEnumerable<OfferModel> GetAll()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return conn.Query<OfferModel>(SelectSql());
            }
        }

        public OfferModel GetbyId(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = SelectSql() + " where offers.id = @id";
                return conn.QuerySingle<OfferModel>(sql, new { id });
            }
        }

        public OfferModel Insert(OfferModel model)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "insert into offers (destination, travelModeId, startDate, endDate, hotelId, price, description, maxParticipants, photo) " +
                    "values (@destination, @modeId, @startDate, @endDate, @hotelId, @price, @description, @maxParticipants, @photo); " +
                    "select last_insert_id()";
                var id = (int)conn.ExecuteScalar(sql, model);
                return GetbyId(id);
            }
        }

        public OfferModel Update(OfferModel model)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "update offers set " +
                    "destination = @destination, " +
                    "travelModeId = @modeId, " +
                    "startDate = @startDate, " +
                    "endDate = @endDate, " +
                    "hotelId = @hotelId, " +
                    "price = @price, " +
                    "description = @description, " +
                    "maxParticipants = @maxParticipants, " +
                    "photo = @photo " +
                    "where id = @id";
                conn.Execute(sql, model);
                return GetbyId(model.Id);
            }
        }

        public void Delete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "delete from offers where id = @id";
                conn.Execute(sql, new { id });
            }
        }
        public IEnumerable<OfferModel> Search(OfferSearchModel model)
        {
            using (var conn = new MySqlConnection(this.connectionString))
            {
                conn.Open();
                var sql = SelectSql();
                var where = "where 0 = 0";
                if (!string.IsNullOrEmpty(model.Destination))
                    where += " and offers.destination like '%' + @destination + '%' ";
                if (model.TravelMode != null)
                    where += " and offers.travelModeId = @modeId ";
                if (model.PriceFrom != null)
                    where += " and offers.price >= @priceFrom ";
                if (model.PriceTo != null)
                    where += " and offers.price <= @priceTo ";
                return conn.Query<OfferModel>(sql, new
                { 
                    model.Destination,
                    modelId = model.TravelMode?.Id,
                    model.PriceFrom,
                    model.PriceTo
                });
                
            }

        }
    }
}
