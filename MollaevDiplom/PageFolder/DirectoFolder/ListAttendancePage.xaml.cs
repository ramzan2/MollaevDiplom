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
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.PageFolder.DirectoFolder
{
    /// <summary>
    /// Логика взаимодействия для ListAttendancePage.xaml
    /// </summary>
    public partial class ListAttendancePage : Page
    {
        public ListAttendancePage()
        {
            Attendees attendees = new Attendees();
            InitializeComponent();
            VariableClass.ListAttendancePage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListAttendeesDT.ItemsSource = DBEntities.GetContext()
        .Attendees.Where(u => u.Meetings.AgendaMeetigns
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.Meetings.AgendaMeetigns);
        }
        private void AddAttBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
