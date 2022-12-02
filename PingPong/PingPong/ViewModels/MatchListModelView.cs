using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.ViewModels
{
    internal class MatchListModelView
    {
        public ObservableCollection<Match> eredmenyek { get; set; }
        public MatchListModelView()
        {
            eredmenyek = new ObservableCollection<Match>();
        }

        public void GetData()
        {
            eredmenyek = App.Database.GetAllMatches();
        }

        
    }
}
