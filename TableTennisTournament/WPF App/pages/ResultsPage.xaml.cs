using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using TableTennis.Data.Models;
using TableTennisWPF.usercontrols;

namespace TableTennisWPF.pages
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetDisplay();
        }

        private bool SetDisplay()
        {
            if (File.Exists("results.json"))
            {

            }
            else if (File.Exists("draw.json"))
            {
                var json = File.ReadAllText("draw.json", Encoding.UTF8);
                var pairs = JsonConvert.DeserializeObject<DrawModel[]>(json);

                for (int i = 0; i < pairs.Length; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    var resultControl = new ResultControl()
                    {
                        Margin = new Thickness(12),
                    };
                    Grid.SetRow(resultControl, i);
                    grid.Children.Add(resultControl);
                }
                var rowSpan = 1;
                for (int i = 0; i <= Math.Log2(pairs.Length); i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    if (i > 0)
                    {
                        for (int j = 0; j < pairs.Length / rowSpan; j++)
                        {
                            var resultControl = new ResultControl()
                            {
                                Margin = new Thickness(12),
                            };
                            Grid.SetColumn(resultControl, i);
                            Grid.SetRowSpan(resultControl, rowSpan);
                            Grid.SetRow(resultControl, j * rowSpan);
                            grid.Children.Add(resultControl);
                        }
                    }
                    rowSpan *= 2;
                }

            }
            else
                return false;
            return true;

        }
    }
}
