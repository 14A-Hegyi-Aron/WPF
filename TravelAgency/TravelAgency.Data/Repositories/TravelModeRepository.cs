using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TravelAgency.Data.Models;

namespace TravelAgency.Data.Repositories
{
    public class TravelModeRepository: GenericRepository<TravelModeModel>
    {
        public TravelModeRepository(string fileName = "travelmode.json") : base(fileName)
        { }

    }
}
