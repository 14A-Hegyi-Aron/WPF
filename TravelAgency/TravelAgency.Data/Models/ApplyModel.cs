using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelAgency.Data.Models
{
    public class ApplyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPeople { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int OfferId { get; set; }
    }
}
