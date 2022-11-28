using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelAgency.Data.Models
{
    public class OfferSearchModel
    {
        public string Destination { get; set; } = "";
        public TravelModeModel TravelMode { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
