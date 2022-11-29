using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TravelAgency.Data.Models;
using TravelAgency.MySQL.EF.Data;

namespace TravelAgency.Data.Repositories
{
    public class TravelModeRepository: GenericRepository<TravelModeModel>
    {
        public TravelModeRepository()
        {
            
        }
        public TravelModeRepository(TravelAgencyDbContext dbContext) : base(dbContext)
        { }

    }
}
