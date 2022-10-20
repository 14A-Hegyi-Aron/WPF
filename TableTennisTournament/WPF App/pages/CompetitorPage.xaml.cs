using Newtonsoft.Json;
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
using TableTennisWPF.extensions;

namespace TableTennisWPF.pages
{
    /// <summary>
    /// Interaction logic for CompetitorPage.xaml
    /// </summary>
    public partial class CompetitorPage : Page
    {
        enum EditModeEnum { List, New, Modify }

        private EditModeEnum _editMode;

        ObservableCollection<CompetitorModel> Competitors { get; set; }
        readonly CompetitorRepository repository;

        public CompetitorPage()
        {
            InitializeComponent();
            repository = new CompetitorRepository();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EditMode = EditModeEnum.List;
            Competitors = new ObservableCollection<CompetitorModel>(repository.GetAll());
            lbCompetitors.ItemsSource = Competitors;

        }
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            EditMode = EditModeEnum.New;
            grpData.DataContext = new CompetitorModel() { BirthDate = DateTime.Today };
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (lbCompetitors.SelectedItem != null)
            {
                var competitor = (CompetitorModel)lbCompetitors.SelectedItem;
                grpData.DataContext = competitor.DeepCopy();




                EditMode = EditModeEnum.Modify;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (lbCompetitors.SelectedItem != null)
            {
                var competitor = (CompetitorModel)lbCompetitors.SelectedItem;
                var msg = $"Biztos törölni szeretné {competitor.Name}-t?";
                if (MessageBox.Show(msg, "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    repository.Delete(competitor.Id);
                    Competitors.Remove(competitor);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var competitor = (CompetitorModel)grpData.DataContext;
            if (HasError(competitor))
                return;

            if (EditMode == EditModeEnum.New)
            {
                var savedCompetitor = repository.Create(competitor);
                Competitors.Add(savedCompetitor);
                grpData.DataContext = null;
            }
            else
            {
                var savedCompetitor = repository.Update(competitor);
                var originalCompetitor = Competitors.Single(c => c.Id == savedCompetitor.Id);
                originalCompetitor = savedCompetitor;
                var index = Competitors.IndexOf(originalCompetitor);
                Competitors.RemoveAt(index);
                Competitors.Insert(index, savedCompetitor);
                grpData.DataContext = null;
            }


            EditMode = EditModeEnum.List;
        }

        private bool HasError(CompetitorModel competitor)
        {
            foreach (var item in ((Grid)grpData.Content).Children.OfType<Control>())
            {
                if (Validation.GetHasError((DependencyObject)item))
                {
                    MessageBox.Show("Hibás adatok");
                    return true;
                }
            }
            if (string.IsNullOrWhiteSpace(competitor.Name))
            {
                MessageBox.Show("Név megadása kötelező");
                return true;
            }
            if (competitor.BirthDate == null)
            {
                MessageBox.Show("Születési dátum megadása kötelező");
                return true;
            }
            if (competitor.Number <= 0)
            {
                MessageBox.Show("Rossz szám");
                return true;
            }
            return false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            grpData.DataContext = null;
            EditMode = EditModeEnum.List;
        }


        private EditModeEnum EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
                grpData.IsEnabled = _editMode != EditModeEnum.List;
                lbCompetitors.IsEnabled = _editMode == EditModeEnum.List;
                panelButtons.IsEnabled = _editMode == EditModeEnum.List;
            }
        }

        private void lbCompetitors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCompetitors.SelectedItem != null)
            {
                var competitor = (CompetitorModel)lbCompetitors.SelectedItem;
                grpData.DataContext = competitor;
            }
            else
            {
                grpData.DataContext = null;
            }
        }
    }
}
