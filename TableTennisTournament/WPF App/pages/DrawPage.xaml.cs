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
using TableTennis.Data.Models;
using TableTennis.Data.Repositories;
using TableTennisWPF.usercontrols;

namespace TableTennisWPF.pages
{
    /// <summary>
    /// Interaction logic for DrawPage.xaml
    /// </summary>
    public partial class DrawPage : Page
    {
        public ObservableCollection<CompetitorModel> Competitors { get; set; }
        private readonly CompetitorRepository repository;
        public DrawPage()
        {
            InitializeComponent();
            repository = new CompetitorRepository();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Competitors = new ObservableCollection<CompetitorModel>(repository.GetAll());
            lbCompetitors.ItemsSource = Competitors;
            int n = Competitors.Count;
            int pairs = (int)Math.Pow(2, Math.Ceiling(Math.Log2(n)) - 1);

            for (int i = 0; i < pairs; i++)
            {
                var pair = new DrawPair()
                {
                    AllowDrop = true,
                };
                panelPairs.Children.Add(pair);
            }
        }

        private void lbCompetitors_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && lbCompetitors.SelectedItem != null)
            {
                var competitor = (CompetitorModel)lbCompetitors.SelectedItem;
                DragDrop.DoDragDrop(lbCompetitors, competitor, DragDropEffects.Move);
            }
        }
    }
}
