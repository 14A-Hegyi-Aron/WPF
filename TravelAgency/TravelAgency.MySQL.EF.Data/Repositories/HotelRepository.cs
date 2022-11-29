using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Data.Models;
using TravelAgency.MySQL.EF.Data;

namespace TravelAgency.Data.Repositories
{
    public class HotelRepository: GenericRepository<HotelModel>
    {
        public HotelRepository()
        {
            
        }
        public HotelRepository(TravelAgencyDbContext dbContext) : base(dbContext)
        { 
            
        }
    }
}
