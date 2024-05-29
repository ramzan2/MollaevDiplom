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
using MollaevDiplom.WindowFolder.AdminWindowFolder;
using MollaevDiplom.WindowFolder.DirectorWindowFolder;

namespace MollaevDiplom.PageFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для ListUserStaffPage.xaml
    /// </summary>
    public partial class ListUserStaffPage : Page
    {
        public ListUserStaffPage()
        {
            Staff staff = new Staff();
            InitializeComponent();
            VariableClass.ListUserStaffPage1 = this;
            SortCB.Text = "Должность";
            UpdateList();
        }
        public void UpdateList()
        {
            ListStaffLb.ItemsSource = DBEntities.GetContext()
          .Staff.Where(u => u.LastNameStaff
          .StartsWith(SearchBox.Text))
          .ToList().OrderBy(u => u.FirstNameStaff);

            IQueryable<Staff> sortList = DBEntities.GetContext().Staff;

            SortselectedCombobox(ref sortList);

            sortList = sortList.Where(u => u.LastNameStaff.Contains(SearchBox.Text)
                                        || u.FirstNameStaff.Contains(SearchBox.Text)
                                        || u.MiddleNameStaff.Contains(SearchBox.Text)
                                        || u.PhoneNumberStaff.Contains(SearchBox.Text)
                                        || u.Position.NamePosition.Contains(SearchBox.Text)
                                        || u.Departments.NameDepartments.Contains(SearchBox.Text)
                                        || u.StatusStaff.NameStatusStaff.Contains(SearchBox.Text));

            sortList = sortList.OrderByDescending(u => u.IdStaff);
            ListStaffLb.ItemsSource = sortList.ToList();

        }
        private void SortselectedCombobox(ref IQueryable<Staff> sortList)
        {

            switch (SortCB.Text)
            {
                case "Секретарь":
                    sortList = sortList.Where(u => u.IdPosition == 1);
                    break;
                case "Администратор":
                    sortList = sortList.Where(u => u.IdPosition == 2);
                    break;
                case "Менеджер":
                    sortList = sortList.Where(u => u.IdPosition == 3);
                    break;
            }
        }
        private void EditTE_Click(object sender, RoutedEventArgs e)
        {
            if (ListStaffLb.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "пользователя для редактирования");
            }
            else
            {
                new EditUserWindow(ListStaffLb.SelectedItem as Staff).Show();
                if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.UpdateList();
            }
        }

        private void InfoTE_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.AdminFolder.InfoUserPage(ListStaffLb.SelectedItem as Staff));
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

        private void AddStBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().Show();
            if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.UpdateList();
        }
    }
}
