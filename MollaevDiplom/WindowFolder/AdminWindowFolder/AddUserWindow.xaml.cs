using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        User user = new User();
        public AddUserWindow()
        {
            InitializeComponent();
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
            var checkLogin = DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == LoginTb.Text);
            if (checkLogin != null)
            {
                MBClass.ErrorMB("Логин занят");
                PasswordTb.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(RoleCb.Text))
            {
                MBClass.ErrorMB("Выберите роль");
                RoleCb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(StatusCb.Text))
            {
                MBClass.ErrorMB("Выберите статус");
                StatusCb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(StaffCb.Text))
            {
                MBClass.ErrorMB("Выберите сотрудника");
                StaffCb.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().User.Add(new User()
                    {
                        LoginUser = LoginTb.Text,
                        PasswordUser = PasswordTb.Text,
                        IdRole = Int32.Parse(RoleCb.SelectedValue.ToString()),
                        IdStaff = Convert.ToInt32(StaffCb.SelectedValue),
                        IdStatusUser = Convert.ToInt32(StatusCb.SelectedValue)
                    }); ;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMB("Пользователь успешно добавлен");
                    if (VariableClass.ListUserPage1 != null) VariableClass.ListUserPage1.UpdateList();
                    if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.Update();
                    Close();
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                    throw;

                }
                Close();
            }
        }
    }
}
