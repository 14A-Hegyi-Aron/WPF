using TableTennisWPF.pages;
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

namespace TableTennisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frame.Content = new HomePage();
        }

        private void MenuCompetitor_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new CompetitorPage();
        }

        private void MenuDraw_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new DrawPage();
        }
    }
}
