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

        public IEnumerable<OfferModel> Search(OfferSearchModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OfferModel> GetAll()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "select offers.id, offers.destination, offers.modeId, offers.startDate, " +
                    "offers.endDate, offers.hotelId, offers.price, offers.description, offers.maxParticipants, " +
                    "offers.photo, travelMode.Name as modeName, hotels.name as hotelName from offers" +
                    "left join travelModes on offers.modeId = travelModes.id " +
                    "left join hotels on offers.hotelId = hotels.id";

            }
        }

        public OfferModel Insert(OfferModel model)
        {
            throw new NotImplementedException();
        }

        public OfferModel Update(OfferModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
