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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgencyWPF.userControls
{
    /// <summary>
    /// Interaction logic for RatingControl.xaml
    /// </summary>
    public partial class RatingControl : UserControl
    {
        private int _max = 0;

        public int Max 
        { 
            get => _max;
            set
            {
                while (_max < value)
                {
                    var rb = new RadioButton()
                    {
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    panel.Children.Insert(_max, rb);

                    rb.Checked += (sender, e) =>
                    {
                        if (rb.IsChecked == true)
                            this.Value = panel.Children.OfType<RadioButton>().ToList().IndexOf(rb) + 1;
                    };
                    _max++;
                }
                while (value < _max)
                {
                    _max--;
                    panel.Children.RemoveAt(_max);
                }
            }
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set 
            {
                if (_max < value)
                    _value = _max;
                else if (value < 0)
                    _value = 0;
                else
                    _value = value;

                if (_value != 0)
                {
                    panel.Children.OfType<RadioButton>().ToList()[_value - 1].IsChecked = true;
                    lblRating.Content = $"{_value}*";
                }
                else
                {
                    panel.Children.OfType<RadioButton>().ToList().ForEach(r => r.IsChecked = false);
                    lblRating.Content = "";
                }
            }
        }


        public RatingControl()
        {
            InitializeComponent();
            Max = 7;
            
        }
    }
}
