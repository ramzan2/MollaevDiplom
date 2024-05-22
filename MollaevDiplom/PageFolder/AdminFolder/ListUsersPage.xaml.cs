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
            User user = new User();
            InitializeComponent();
            VariableClass.ListUserPage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListMeetingsDT.ItemsSource = DBEntities.GetContext()
          .User.Where(u => u.LoginUser
          .StartsWith(SearchBox.Text))
          .ToList().OrderBy(u => u.LoginUser);

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
                    "участника для редактирования");
            }
            else
            {
                new EditUserWindow(ListMeetingsDT.SelectedItem as User).Show();
                if(VariableClass.menuAdminWindow != null)VariableClass.menuAdminWindow.UpdateList();
                UpdateList();
            }
        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {
            User user = ListMeetingsDT.SelectedItem as User;
            if (ListMeetingsDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить " +
                    $"пользователя " +
                    $"{user.LoginUser}?"))
                {
                    DBEntities.GetContext().User
                        .Remove(ListMeetingsDT.SelectedItem as User);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InfoMB("Пользователь удалена");
                    ListMeetingsDT.ItemsSource = DBEntities.GetContext()
                        .User.ToList().OrderBy(u => u.LoginUser);
                }
            }
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().Show();
            if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.UpdateList();
        }
    }
}
