using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class OfferRepository : GenericRepository<OfferModel>
    {
        public OfferRepository(string fileName = "offers.json") : base(fileName)
        { }
    }
}
