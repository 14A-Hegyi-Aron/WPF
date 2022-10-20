using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableTennis.Data.Models
{
    public class CompetitorModel
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Rank { get; set; } = 1;

    }
}
