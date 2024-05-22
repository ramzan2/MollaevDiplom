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
    /// Логика взаимодействия для EditMeetingsWindow.xaml
    /// </summary>
    public partial class EditMeetingsWindow : Window
    {
        private Meetings originalMeetings;
        public EditMeetingsWindow(Meetings existingMeetings)
        {
            InitializeComponent();
            DBEntities.NullContext();
            originalMeetings = DBEntities.GetContext().Meetings
                .FirstOrDefault(u => u.IdMeetings == existingMeetings.IdMeetings);
            DataContext = originalMeetings;
            this.originalMeetings.IdMeetings = originalMeetings.IdMeetings;

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

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddMtBtn_Click(object sender, RoutedEventArgs e)
        {
            originalMeetings = DBEntities.GetContext().Meetings
                    .FirstOrDefault(u => u.IdMeetings == originalMeetings.IdMeetings);
            originalMeetings.MeetinsDate = Convert.ToDateTime(MetDateTb.SelectedDate);
            originalMeetings.MeetingsTime = MeetingsTimeTb.Text;
            originalMeetings.MeetingsDuration = MeetingsDurationTb.Text;
            originalMeetings.AgendaMeetigns = AgendaMeetignsTb.Text;
            originalMeetings.IdStaff = Int32.Parse(LastNameCb.SelectedValue.ToString());
            originalMeetings.IdDepartments = Int32.Parse(DepartmentsCb.SelectedValue.ToString());
            originalMeetings.IdStatusMeetings = Int32.Parse(StatusCb.SelectedValue.ToString());
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Данные успешно отредактированы");
            if (VariableClass.ListMeetingsPage1 != null) VariableClass.ListMeetingsPage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            Close();
        }
    }
}
