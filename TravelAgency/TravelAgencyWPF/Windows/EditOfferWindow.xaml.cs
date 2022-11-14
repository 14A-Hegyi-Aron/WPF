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
using System.Windows.Shapes;
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;

namespace TravelAgencyWPF.Windows
{
    /// <summary>
    /// Interaction logic for EditOffersWindow.xaml
    /// </summary>
    public partial class EditOffersWindow : Window
    {
        public OfferModel SavedModel { get; set; }
        public EditOffersWindow(OfferModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hotelRepo = new HotelRepository();
            cboHotel.ItemsSource = hotelRepo.GetAll();
            cboHotel.DisplayMemberPath = "Name";
            cboHotel.SelectedValuePath = "Id";

            var travelModeRepo = new TravelModeRepository();
            cboTravelMode.ItemsSource = hotelRepo.GetAll();
            cboTravelMode.DisplayMemberPath = "Name";
            cboTravelMode.SelectedValuePath = "Id";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var model = (OfferModel)this.DataContext;
            if (CheckData(model))
            {
                var repo = new OfferRepository();
                if (model.Id == 0)
                    this.SavedModel = repo.Insert(model);
                else
                    this.SavedModel = repo.Update(model);
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool CheckData(OfferModel model)
        {
            var ok = true;
            lblDestination.Foreground = Brushes.Black;
            lblMode.Foreground = Brushes.Black;
            lblDates.Foreground = Brushes.Black;
            lblHotel.Foreground = Brushes.Black;
            lblPrice.Foreground = Brushes.Black;
            lblMaxParticipants.Foreground = Brushes.Black;

            if (string.IsNullOrWhiteSpace(model.Destination))
            {
                lblDestination.Foreground = Brushes.Red;
                ok = false;
            }
            if (model.ModeId == 0)
            {
                lblMode.Foreground = Brushes.Red;
                ok = false;
            }
            if (model.StartDate == null || model.EndDate == null)
            {
                lblDates.Foreground = Brushes.Red;
                ok = false;
            }
            if (model.HotelId == 0)
            {
                lblHotel.Foreground = Brushes.Red;
                ok = false;
            }
            if (model.Price == 0)
            {
                lblPrice.Foreground = Brushes.Red;
                ok = false;
            }
            if (model.MaxParticipants == 0)
            {
                lblMaxParticipants.Foreground = Brushes.Red;
                ok = false;
            }
            if (ok)
                return !GetHasError(grid);
            return false;
        }

        private bool GetHasError(Panel parentPanel)
        {
            foreach (var uIElement in parentPanel.Children.OfType<UIElement>())
            {
                if (Validation.GetHasError(uIElement))
                    return true;
                if (uIElement is Panel)
                    if (GetHasError((Panel)uIElement))
                        return true;
            }
            return false;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
