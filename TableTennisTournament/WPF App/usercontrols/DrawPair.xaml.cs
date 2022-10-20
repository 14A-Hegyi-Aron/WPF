using System.Windows;
using System.Windows.Controls;
using TableTennis.Data.Models;

namespace TableTennisWPF.usercontrols
{
    /// <summary>
    /// Interaction logic for DrawPair.xaml
    /// </summary>
    public partial class DrawPair : UserControl
    {
        CompetitorModel[] competitors = null;
        public CompetitorModel[] Competitors
        {
            get
            {
                return competitors;
            }
            set
            {
                competitors = value;
                if (competitors.Length == 1)
                {
                    lbName1.Content = competitors[0].Name;
                    lbName2.Visibility = Visibility.Collapsed;
                }
                else if (competitors.Length == 2)
                {
                    lbName1.Content = competitors[0].Name;
                    lbName2.Content = competitors[1].Name;
                }
            }
        }
        public DrawPair()
        {
            InitializeComponent();
        }
    }
}
