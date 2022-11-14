using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;
using TravelAgencyWPF.Windows;

namespace TravelAgencyWPF.pages
{
    /// <summary>
    /// Interaction logic for OffersPage.xaml
    /// </summary>
    public partial class OffersPage : Page
    {
        public OffersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var travelModeRepository = new TravelModeRepository();
            cboTravelMode.ItemsSource = travelModeRepository.GetAll();
            cboTravelMode.DisplayMemberPath = "Name";

            grpSearch.DataContext = new OfferSearchModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var offerRepository = new OfferRepository();
            dgOffers.ItemsSource = new ObservableCollection<OfferModel>
            (
                offerRepository.Search((OfferSearchModel)grpSearch.DataContext)
            );
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            grpSearch.DataContext = new OfferSearchModel();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new EditOffersWindow(new OfferModel());
            if (wnd.ShowDialog() == true)
            {

            }
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Price_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!decimal.TryParse(textBox.Text, out decimal x))
                textBox.Text = null;
        }
    }
}
