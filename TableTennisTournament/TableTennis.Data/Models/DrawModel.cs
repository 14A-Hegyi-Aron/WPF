using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Data.Models
{
    public class DrawModel
    {
        public int TableId { get; set; }
        public CompetitorModel[] Competitors { get; set; }
    }
}
