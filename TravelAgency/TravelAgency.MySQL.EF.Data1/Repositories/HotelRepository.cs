using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class HotelRepository: GenericRepository<HotelModel>
    {
        public HotelRepository(string fileName = "hotels.json" ): base( fileName )
        { }
    }
}
