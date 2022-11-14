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
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.Data.Repositories;

namespace TravelAgencyWPF.windows
{
    /// <summary>
    /// Interaction logic for EditHotelWindow.xaml
    /// </summary>
    public partial class EditHotelWindow : Window
    {
        public HotelModel SavedModel { get; private set; }

        public EditHotelWindow(HotelModel model)
        {
            InitializeComponent();

            this.DataContext = model.DeepCopy();
            ucRating.Value = model.Stars;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var model = (HotelModel)this.DataContext;
            model.Stars = ucRating.Value;

            if (CheckData(model))
            {
                var repository = new HotelRepository();
                if (model.Id == 0)
                    this.SavedModel = repository.Insert(model);
                else
                    this.SavedModel = repository.Update(model);
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool CheckData(HotelModel model)
        {
            var ok = true;
            lblName.Foreground = Brushes.Black;
            lblAddress.Foreground = Brushes.Black;
            lblStars.Foreground = Brushes.Black;
            lblURL.Foreground = Brushes.Black;
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                lblName.Foreground = Brushes.Red;
                ok = false;
            }
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                lblAddress.Foreground = Brushes.Red;
                ok = false;
            }
            if (model.Stars <= 0)
            {
                lblStars.Foreground = Brushes.Red;
                ok = false;
            }
            if (string.IsNullOrWhiteSpace(model.WebPageUrl))
            {
                lblURL.Foreground = Brushes.Red;
                ok = false;
            }
            return ok;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
                lblName.Foreground = Brushes.Red;
            else
                lblName.Foreground = Brushes.Black;
        }
    }
}
