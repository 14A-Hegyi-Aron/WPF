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

namespace TravelAgencyWPF.UserControls
{
    /// <summary>
    /// Interaction logic for RatingControl.xaml
    /// </summary>
    public partial class RatingControl : UserControl
    {
        private int _max;

        public int Max
        {
            get => _max;
            set
            {
                if (_max > value)
                {
                    var rb = new RadioButton();
                    panel.Children.Insert(_max, rb);
                    _max++;
                }
                while (value > _max)
                {
                    _max--;
                    panel.Children.RemoveAt(_max);
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
