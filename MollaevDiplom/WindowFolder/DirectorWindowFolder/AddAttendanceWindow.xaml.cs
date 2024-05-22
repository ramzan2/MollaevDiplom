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
    /// Логика взаимодействия для AddAttendanceWindow.xaml
    /// </summary>
    public partial class AddAttendanceWindow : Window
    {
        Attendees attendees = new Attendees();
        public AddAttendanceWindow()
        {
            InitializeComponent();
            AgendaCb.ItemsSource = DBEntities.GetContext()
                   .Meetings.ToList();
            LastNmCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
            FirstNmCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MiddleNmCb.ItemsSource = DBEntities.GetContext()
          .Staff.ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddAttBtn_Click(object sender, RoutedEventArgs e)
        {
            DBEntities.GetContext().Attendees.Add(new Attendees()
            {
                IdMeetings = Int32.Parse(AgendaCb.SelectedValue.ToString()),
                IdStaff = Int32.Parse(LastNmCb.SelectedValue.ToString())
            });
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Сотрудник успешно добавлен");
            if (VariableClass.ListAttendancePage1 != null) VariableClass.ListAttendancePage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
        }
    }
}
