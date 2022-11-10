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
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;

namespace TravelAgencyWPF.pages
{
    /// <summary>
    /// Interaction logic for OffersPage.xaml
    /// </summary>
    public partial class OffersPage : Page
    {
        private OfferSearchModel searchModel = new();
        public OffersPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TravelModeRepository travelModeRepository = new();
            cboTravelMode.ItemsSource = travelModeRepository.GetAll();
            cboTravelMode.DisplayMemberPath = "Name";

            grpSearch
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
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
            var textBox = sender as TextBox;
            if (!decimal.TryParse(textBox.Text, out decimal x))
            {
                textBox.Text = null;
            }
        }
    }
}
