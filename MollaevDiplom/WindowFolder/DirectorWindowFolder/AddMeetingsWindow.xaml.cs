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
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddMeetingsWindow.xaml
    /// </summary>
    public partial class AddMeetingsWindow : Window
    {
        Meetings meetings = new Meetings();
        public AddMeetingsWindow()
        {
            InitializeComponent();
            DepartmentsCb.ItemsSource = DBEntities.GetContext()
                  .Departments.ToList();
            LastNameCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
            FirstNameCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MiddleNameStaffCb.ItemsSource = DBEntities.GetContext()
          .Staff.ToList();
            StatusCb.ItemsSource = DBEntities.GetContext()
          .StatusMeetings.ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
        }

        private void AddMtBtn_Click(object sender, RoutedEventArgs e)
        {
            DBEntities.GetContext().Meetings.Add(new Meetings()
            {
                MeetinsDate = Convert.ToDateTime(MetDateTb.SelectedDate),
                MeetingsTime = MeetingsTimeTb.Text,
                MeetingsDuration = MeetingsDurationTb.Text,
                AgendaMeetigns = AgendaMeetignsTb.Text,
                IdStaff = Convert.ToInt32(LastNameCb.SelectedValue),
                IdDepartments = Convert.ToInt32(DepartmentsCb.SelectedValue),
                IdStatusMeetings = Convert.ToInt32(StatusCb.SelectedValue)
            });
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Сотрудник успешно добавлен");
            if (VariableClass.ListMeetingsPage1 != null) VariableClass.ListMeetingsPage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            Close();
        }
    }
}
