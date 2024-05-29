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
using MollaevDiplom.WindowFolder.DirectorWindowFolder;

namespace MollaevDiplom.PageFolder.DirectoFolder
{
    /// <summary>
    /// Логика взаимодействия для ListMeetingsPage.xaml
    /// </summary>
    public partial class ListMeetingsPage : Page
    {
        public ListMeetingsPage()
        {
            Meetings meetings = new Meetings();
            InitializeComponent();
            SortCB.Text = "Статус";
            VariableClass.ListMeetingsPage1 = this;
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

        private void AddMtBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddMeetingsWindow().Show();
            if(VariableClass.direcWindow != null)VariableClass.direcWindow.UpdateList();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (ListMeetingsDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "встречу для редактирования");
            }
            else
            {
                new EditMeetingsWindow(ListMeetingsDT.SelectedItem as Meetings).Show();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
            }
        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {
            Meetings meetings = ListMeetingsDT.SelectedItem as Meetings;

            if (ListMeetingsDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите встречу" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить " +
                    $"документ? {meetings.AgendaMeetigns} " +
                    $"{meetings.AgendaMeetigns} {meetings.AgendaMeetigns}?"))
                {
                    DBEntities.GetContext().Meetings
                        .Remove(ListMeetingsDT.SelectedItem as Meetings);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InfoMB("Встреча удалена");
                    ListMeetingsDT.ItemsSource = DBEntities.GetContext()
                        .Meetings.ToList().OrderBy(r => r.AgendaMeetigns);
                }

            }
        }

        private async void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(1);
            UpdateList();
        }
    }
}
