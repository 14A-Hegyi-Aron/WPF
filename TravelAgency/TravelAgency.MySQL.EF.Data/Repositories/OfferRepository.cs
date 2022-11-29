using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;
using TravelAgency.MySQL.EF.Data;

namespace TravelAgency.Data.Repositories
{
    public class OfferRepository: GenericRepository<OfferModel>
    {
        public OfferRepository()
        {
            
        }
        public OfferRepository(TravelAgencyDbContext dbContext) : base(dbContext)
        {  }

        public IEnumerable<OfferModel> Search(OfferSearchModel model)
        {
            return dbContext.Set<OfferModel>().Where
            (
                o => (o.Destination.Contains(model.Destination))
                  && (model.TravelMode == null || o.ModeId == model.TravelMode.Id)
                  && (model.PriceFrom == null || o.Price >= model.PriceFrom)
                  && (model.PriceTo == null || o.Price <= model.PriceTo)
            ).ToList();
        }
    }
}
