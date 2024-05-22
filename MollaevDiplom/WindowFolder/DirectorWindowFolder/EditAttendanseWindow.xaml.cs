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
    /// Логика взаимодействия для EditAttendanseWindow.xaml
    /// </summary>
    public partial class EditAttendanseWindow : Window
    {
        private Attendees originalAttendees;
        public EditAttendanseWindow(Attendees existingAttendees)
        {
            InitializeComponent();
            DBEntities.NullContext();
            originalAttendees = DBEntities.GetContext().Attendees
                .FirstOrDefault(u => u.IdAttendees == existingAttendees.IdAttendees);


            DataContext = originalAttendees;
            this.originalAttendees.IdAttendees = originalAttendees.IdAttendees;


            AgendaCb.ItemsSource = DBEntities.GetContext()
                       .Meetings.ToList();
            LastNmCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();

            FirstNmCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
            MiddleNmCb.ItemsSource = DBEntities.GetContext()
                  .Staff.ToList();
        }

        private void SaveAttBtn_Click(object sender, RoutedEventArgs e)
        {
            originalAttendees = DBEntities.GetContext().Attendees
                    .FirstOrDefault(u => u.IdAttendees == originalAttendees.IdAttendees);
            originalAttendees.IdMeetings = Int32.Parse(AgendaCb.SelectedValue.ToString());
            originalAttendees.IdStaff = Int32.Parse(LastNmCb.SelectedValue.ToString());
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Данные успешно отредактированы");
            if (VariableClass.ListAttendancePage1 != null) VariableClass.ListAttendancePage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
