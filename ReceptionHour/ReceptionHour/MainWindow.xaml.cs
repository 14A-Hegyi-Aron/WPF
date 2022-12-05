using ReceptionHour.Data.Models;
using ReceptionHour.Data.Repositories;
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

namespace ReceptionHour
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MeetingRepository meetingRepository;
        private ObservableCollection<MeetingModel> meetings => (ObservableCollection<MeetingModel>)dgMeetings.ItemsSource;
        public MainWindow()
        {
            InitializeComponent();
            meetingRepository = new MeetingRepository();

            Loaded += (sender, e) =>
            {
                cboTeacher.ItemsSource = new TeacherRepository().GetAll();
                cboTeacher.DisplayMemberPath = "Name";
            };
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            dgMeetings.ItemsSource = new ObservableCollection<MeetingModel>
                (
                    meetingRepository.Search
                    (
                        (TeacherModel)cboTeacher.SelectedItem, dpDate.SelectedDate
                        )
                );
            meetingRepository.Search((TeacherModel)cboTeacher.SelectedItem, dpDate.SelectedDate);
        }
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            cboTeacher.SelectedIndex = -1;
            dpDate.SelectedDate = null;
            dgMeetings.ItemsSource = null;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new MeetingWindow(new MeetingModel());
            if (wnd.ShowDialog() == true)
            {
                meetings.Add(wnd.Meeting);
            }
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgMeetings.SelectedItem != null)
            {
                var meeting = (MeetingModel)dgMeetings.SelectedItem;
                var wnd = new MeetingWindow(new MeetingModel()
                {
                    Id = meeting.Id,
                    TeacherId = meeting.TeacherId,
                    Date = meeting.Date,
                    ParentName = meeting.ParentName,
                });
                if (wnd.ShowDialog() == true)
                {
                    var index = meetings.IndexOf(meeting);
                    meetings[index] = wnd.Meeting;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgMeetings.SelectedItem != null)
            {
                var meeting = (MeetingModel)dgMeetings.SelectedItem;
                if (MessageBox.Show($"Are you sure you want to delete {meeting.ParentName}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    meetingRepository.Delete(meeting);
                    meetings.Remove(meeting);
                }
            }
        }


    }
}
