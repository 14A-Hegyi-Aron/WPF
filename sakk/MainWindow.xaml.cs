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

namespace sakk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Figure> figures = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddColAndRow();
            AddLabels();
            AddChessFields();

            InitialPosition();
        }

        private void InitialPosition()
        {
            for (int i = 0; i <= 8; i++)
            {
                figures.Add(new Pawn(Color.White, 7, i, grid));
                figures.Add(new Pawn(Color.Black, 2, i, grid));
            }
            figures.Add(new Knight(Color.White, 8, 2, grid));
            figures.Add(new Knight(Color.Black, 1, 2, grid));



            figures.ForEach(f => f.Activated += Figure_Activated);
        }

        private void Figure_Activated(object? sender, EventArgs e)
        {
            foreach (var figure in figures)
            {
                if (sender != figure && figure.Active)
                    figure.Active = false;
            }
        }

        private void AddColAndRow()
        {
            grid.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(40)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(30)
            });
            for (int i = 0; i < 9; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void AddLabels()
        {
            for (int i = 1; i <= 8; i++)
            {
                Label lb = new Label();
                lb.Content = (char)(i + 64);
                lb.HorizontalContentAlignment = HorizontalAlignment.Center;
                lb.VerticalContentAlignment = VerticalAlignment.Center;
                lb.FontWeight = FontWeights.Bold;

                grid.Children.Add(lb);
                Grid.SetColumn(lb, i);

                lb = new Label()
                {
                    Content = 9 - i,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };

                grid.Children.Add(lb);
                Grid.SetRow(lb, i);
            }
        }
        private void AddChessFields()
        {
            for (int row = 1; row <= 8; row++)
                for (int col = 1; col <= 8; col++)
                {
                    var panel = new WrapPanel()
                    {
                        Background = (row + col) % 2 == 0 ? Brushes.DodgerBlue : Brushes.DarkBlue
                    };

                    panel.MouseLeftButtonDown += Panel_MouseLeftButtonDown;

                    grid.Children.Add(panel);
                    Grid.SetRow(panel, row);
                    Grid.SetColumn(panel, col);
                }

        }

        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var panel = (WrapPanel)sender;
            var row = Grid.GetRow(panel);
            var col = Grid.GetColumn(panel);
            //Figure? activeFigure = null;

            //foreach (var figure in figures)
            //{
            //    if (figure.Active)
            //    {
            //        activeFigure = figure;
            //        break;
            //    }
            //}

            var activeFigure = figures.SingleOrDefault(figure => figure.Active);
            if (activeFigure != null && (row != activeFigure.Row || col != activeFigure.Col))
            {
                if (panel.Background == Brushes.Magenta || panel.Background == Brushes.DarkMagenta)
                {
                    activeFigure.Active = false;
                    activeFigure.Move(row, col);
                }
            }
        }
    }
}
