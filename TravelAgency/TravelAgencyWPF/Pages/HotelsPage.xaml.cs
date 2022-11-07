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
using TravelAgencyWPF.windows;

namespace TravelAgencyWPF.pages
{
    /// <summary>
    /// Interaction logic for HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {

        ObservableCollection<HotelModel> hotels = null;
        HotelRepository repository = new HotelRepository();

        public HotelsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            hotels = new ObservableCollection<HotelModel>(repository.GetAll());
            dgHotels.ItemsSource = hotels;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new EditHotelWindow(new HotelModel());
            if (wnd.ShowDialog() == true)
            {
                hotels.Add(wnd.SavedModel);
            }
            
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (dgHotels.SelectedItem != null)
            {
                var model = (HotelModel)dgHotels.SelectedItem;
                var wnd = new EditHotelWindow(model);
                if (wnd.ShowDialog() == true)
                {
                    var index = hotels.IndexOf(model);
                    hotels[index] = wnd.SavedModel;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgHotels.SelectedItem != null)
            {
                var model = (HotelModel)dgHotels.SelectedItem;
                if (MessageBox.Show($"Are you sure you want to delete {model.Name}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    repository.Delete(model.Id);
                    hotels.Remove(model);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
