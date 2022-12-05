using ReceptionHour.Data.Models;
using ReceptionHour.Data.Repositories;
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

namespace ReceptionHour
{
    /// <summary>
    /// Interaction logic for MeetingWindow.xaml
    /// </summary>
    public partial class MeetingWindow : Window
    {
        public MeetingModel Meeting { get; private set; }
        public MeetingWindow(MeetingModel meetingModel)
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                cboTeacher.ItemsSource = new TeacherRepository().GetAll();
                cboTeacher.DisplayMemberPath = "Name";
                cboTeacher.SelectedValuePath = "Id";
                DataContext = meetingModel;
            };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var meeting = (MeetingModel)DataContext;
            if (cboTeacher.SelectedIndex == -1)
            {
                cboTeacher.IsDropDownOpen = true;
                return;
            }
            if (meeting.Date == null)
            {
                dpDate.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(meeting.ParentName))
            {
                txtParentName.Focus();
                return;
            }
            if (meeting.Id == 0)
                Meeting = new MeetingRepository().Insert(meeting);
            else
                Meeting = new MeetingRepository().Update(meeting);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
