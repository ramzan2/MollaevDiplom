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
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.PageFolder.SecretaryFolder
{
    /// <summary>
    /// Логика взаимодействия для ListAttSecPage.xaml
    /// </summary>
    public partial class ListAttSecPage : Page
    {
        public ListAttSecPage()
        {
            Attendees attendees = new Attendees();
            InitializeComponent();
            UpdateList();
        }
        public void UpdateList()
        {
            ListAttendeesDT.ItemsSource = DBEntities.GetContext()
        .Attendees.Where(u => u.Meetings.AgendaMeetigns
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.Meetings.AgendaMeetigns);
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
