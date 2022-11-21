using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class OfferRepository: GenericRepository<OfferModel>
    {
        public OfferRepository(string fileName = "offers.json"): base(fileName)
        {  }

        public IEnumerable<OfferModel> Search(OfferSearchModel model)
        {
            return this.GetAll().Where
            (
                o => (o.Destination.Contains(model.Destination))
                  && (model.TravelMode == null || o.ModeId == model.TravelMode.Id)
                  && (model.PriceFrom == null || o.Price >= model.PriceFrom)
                  && (model.PriceTo == null || o.Price <= model.PriceTo)
            );
        }
    }
}
