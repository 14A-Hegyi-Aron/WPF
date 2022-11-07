using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelAgency.Data.Models
{
    public class TravelModeModel: IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
