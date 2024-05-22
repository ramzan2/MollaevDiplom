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

namespace MollaevDiplom.WindowFolder.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        User originalUser = new User();
        public EditUserWindow(User user)
        {
            InitializeComponent();
            DataContext = originalUser;
            DBEntities.NullContext();
            originalUser = DBEntities.GetContext().User
                .FirstOrDefault(u => u.IdUser == originalUser.IdUser);
            RoleCb.ItemsSource = DBEntities.GetContext()
                .Role.ToList();
            StatusCb.ItemsSource = DBEntities.GetContext()
                   .StatusUser.ToList();
            StaffCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.Update();
        }

        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                originalUser = DBEntities.GetContext().User
                   .FirstOrDefault(u => u.IdUser == originalUser.IdUser);
                originalUser.LoginUser = LoginTb.Text;
                originalUser.PasswordUser = PasswordTb.Text;
                originalUser.IdRole = Int32.Parse(
                    RoleCb.SelectedValue.ToString());
                originalUser.IdStaff = Int32.Parse(
                    StaffCb.SelectedValue.ToString());
                originalUser.IdStatusUser = Int32.Parse(
                    StatusCb.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Данные успешно отредактированы");
                if (VariableClass.ListUserPage1 != null) VariableClass.ListUserPage1.UpdateList();
                if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.Update();
                Close();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}
