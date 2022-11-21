using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelAgency.Data.Models
{
    public class HotelModel: IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string WebPageUrl { get; set; }
        public string Description { get; set; }
    }
}
