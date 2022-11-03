using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgencyWPF.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Hotels_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Content = new HotelsPage();
        }

        private void Offers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Content = new OffersPage();
        }
    }
}
