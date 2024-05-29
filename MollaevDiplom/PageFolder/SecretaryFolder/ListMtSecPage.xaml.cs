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
    /// Логика взаимодействия для ListMtSecPage.xaml
    /// </summary>
    public partial class ListMtSecPage : Page
    {
        public ListMtSecPage()
        {
            Meetings meetings = new Meetings();
            InitializeComponent();
            SortCB.Text = "Статус";
            UpdateList();
        }

        public void UpdateList()
        {
            ListMeetingsDT.ItemsSource = DBEntities.GetContext()
        .Meetings.Where(u => u.AgendaMeetigns
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.AgendaMeetigns);

            IQueryable<Meetings> sortList = DBEntities.GetContext().Meetings;

            SortselectedCombobox(ref sortList);

            sortList = sortList.Where(u => u.MeetingsTime.Contains(SearchBox.Text)
                                        || u.MeetingsDuration.Contains(SearchBox.Text)
                                        || u.AgendaMeetigns.Contains(SearchBox.Text)
                                        || u.Staff.LastNameStaff.Contains(SearchBox.Text)
                                        || u.Departments.NameDepartments.Contains(SearchBox.Text)
                                        || u.StatusMeetings.NameStatusMeetings.Contains(SearchBox.Text));

            sortList = sortList.OrderByDescending(u => u.IdStaff);
            ListMeetingsDT.ItemsSource = sortList.ToList();
        }

        private void SortselectedCombobox(ref IQueryable<Meetings> sortList)
        {

            switch (SortCB.Text)
            {
                case "Идет":
                    sortList = sortList.Where(u => u.IdStatusMeetings == 2);
                    break;
                case "Ожидается":
                    sortList = sortList.Where(u => u.IdStatusMeetings == 3);
                    break;
                case "Оконченно":
                    sortList = sortList.Where(u => u.IdStatusMeetings == 1);
                    break;
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private async void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(1);
            UpdateList();
        }
    }
}
