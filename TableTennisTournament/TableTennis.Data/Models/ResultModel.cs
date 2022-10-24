using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Data.Models
{
    public class ResultModel
    {
        public CompetitorModel[] Competitors { get; set; }
        public Tuple<int, int>[] Matches { get; set; }
        public int? WinnerIndex { get; set; }
    }
}
