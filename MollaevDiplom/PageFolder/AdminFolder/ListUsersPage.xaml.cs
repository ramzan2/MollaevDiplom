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
    /// Логика взаимодействия для ListUsersPage.xaml
    /// </summary>
    public partial class ListUsersPage : Page
    {
        public ListUsersPage()
        {
            Staff staff = new Staff();
            InitializeComponent();
            SortCB.Text = "Статус";
            VariableClass.ListUserPage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListMeetingsDT.ItemsSource = DBEntities.GetContext()
          .Staff.Where(u => u.LastNameStaff
          .StartsWith(SearchBox.Text))
          .ToList().OrderBy(u => u.LastNameStaff);


            IQueryable<Staff> sortList = DBEntities.GetContext().Staff;

            SortselectedCombobox(ref sortList);

            sortList = sortList.Where(u => u.LastNameStaff.Contains(SearchBox.Text)
                                        || u.FirstNameStaff.Contains(SearchBox.Text)
                                        || u.MiddleNameStaff.Contains(SearchBox.Text)
                                        || u.PhoneNumberStaff.Contains(SearchBox.Text)
                                        || u.Position.NamePosition.Contains(SearchBox.Text)
                                        || u.Departments.NameDepartments.Contains(SearchBox.Text)
                                        || u.StatusStaff.NameStatusStaff.Contains(SearchBox.Text)
                                        || u.User.StatusUser.NameStatusUser.Contains(SearchBox.Text));

            sortList = sortList.OrderByDescending(u => u.IdUser);
            ListMeetingsDT.ItemsSource = sortList.ToList();
        }
        public void Update()
        {
            IQueryable<Staff> sortList = DBEntities.GetContext().Staff;

            SortselectedCombobox(ref sortList);

            sortList = sortList.Where(u => u.LastNameStaff.Contains(SearchBox.Text)
                                        || u.FirstNameStaff.Contains(SearchBox.Text)
                                        || u.MiddleNameStaff.Contains(SearchBox.Text)
                                        || u.PhoneNumberStaff.Contains(SearchBox.Text)
                                        || u.Position.NamePosition.Contains(SearchBox.Text)
                                        || u.Departments.NameDepartments.Contains(SearchBox.Text)
                                        || u.StatusStaff.NameStatusStaff.Contains(SearchBox.Text)
                                        || u.User.StatusUser.NameStatusUser.Contains(SearchBox.Text));

            sortList = sortList.OrderByDescending(u => u.IdUser);
            ListMeetingsDT.ItemsSource = sortList.ToList();
        }
        private void SortselectedCombobox(ref IQueryable<Staff> sortList)
        {

            switch (SortCB.Text)
            {
                case "Активен":
                    sortList = sortList.Where(u => u.IdStatusStaff == 2);
                    break;
                case "Не действителен":
                    sortList = sortList.Where(u => u.IdStatusStaff == 3);
                    break;
                case "Заблокирован":
                    sortList = sortList.Where(u => u.IdStatusStaff == 1);
                    break;
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (ListMeetingsDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "пользователя для редактирования");
            }
            else
            {
                new EditUserWindow(ListMeetingsDT.SelectedItem as Staff).Show();
                if(VariableClass.menuAdminWindow != null)VariableClass.menuAdminWindow.UpdateList();
                UpdateList();
            }
        }

        //private void DeleteIM_Click(object sender, RoutedEventArgs e)
        //{
        //    User user = ListMeetingsDT.SelectedItem as User;
        //    if (ListMeetingsDT.SelectedItem == null)
        //    {
        //        MBClass.ErrorMB("Выберите пользователя" +
        //            " для удаления");
        //    }
        //    else
        //    {
        //        if (MBClass.QuestionMB("Удалить " +
        //            $"пользователя " +
        //            $"{user.LoginUser}?"))
        //        {
        //            DBEntities.GetContext().User
        //                .Remove(ListMeetingsDT.SelectedItem as User);
        //            DBEntities.GetContext().SaveChanges();

        //            MBClass.InfoMB("Пользователь удалена");
        //            ListMeetingsDT.ItemsSource = DBEntities.GetContext()
        //                .User.ToList().OrderBy(u => u.LoginUser);
        //        }
        //    }
        //}

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().Show();
            if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.UpdateList();
        }

        private async void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(1);
            UpdateList();
        }
    }
}
