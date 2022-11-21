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
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;
using TravelAgencyWPF.windows;

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
            dgOffers.ItemsSource = new ObservableCollection<OfferModel>();
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
            var wnd = new EditOfferWindow(new OfferModel());
            if (wnd.ShowDialog() == true)
            {
                ((ObservableCollection<OfferModel>)dgOffers.ItemsSource).Add(wnd.SavedModel);
            }
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (dgOffers.SelectedItem != null)
            {
                var model = (OfferModel)dgOffers.SelectedItem;
                var wnd = new EditOfferWindow(model.DeepCopy());
                if (wnd.ShowDialog() == true)
                {
                    var items = (ObservableCollection<OfferModel>)dgOffers.ItemsSource;
                    var index = items.IndexOf(model);
                    items[index] = wnd.SavedModel;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgOffers.SelectedItem != null)
            {
                var model = (OfferModel)dgOffers.SelectedItem;
                var msg = $"Biztosan szeretné törölni a(z) {model.Destination} úti célt?";
                if (MessageBox.Show(msg, "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var repo = new OfferRepository();
                    repo.Delete(model.Id);
                    var items = (ObservableCollection<OfferModel>)dgOffers.ItemsSource;
                    items.Remove(model);
                }
            }
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
