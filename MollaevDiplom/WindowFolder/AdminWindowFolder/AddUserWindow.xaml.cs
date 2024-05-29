using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using Microsoft.Win32;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.WindowFolder.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        Staff staff = new Staff();
        User user = new User();
        public AddUserWindow()
        {
            InitializeComponent();
            RoleCb.ItemsSource = DBEntities.GetContext()
                 .Role.ToList();
            StatusUserCb.ItemsSource = DBEntities.GetContext()
                   .StatusUser.ToList();
            StatusStaffCb.ItemsSource = DBEntities.GetContext()
                   .StatusStaff.ToList();
            DepartmentsCb.ItemsSource = DBEntities.GetContext()
                  .Departments.ToList();
            PositionCb.ItemsSource = DBEntities.GetContext()
                 .Position.ToList();
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
        public void UserAdd()
        {
            var userAdd = new User()
            {
                LoginUser = LoginUserTb.Text,
                PasswordUser = PasswordUserTb.Text,
                IdRole = Int32.Parse(RoleCb.SelectedValue.ToString()),
                IdStatusUser = Convert.ToInt32(StatusUserCb.SelectedValue)
            };
            DBEntities.GetContext().User.Add(userAdd);
            DBEntities.GetContext().SaveChanges();
            user.IdUser = userAdd.IdUser;
        }
        string selectedFileName = "";
        public void StaffAdd()
        {
            var staffAdd = new Staff()
            {
                LastNameStaff = LastNameTb.Text,
                FirstNameStaff = FirstNameTb.Text,
                MiddleNameStaff = MiddleNameTb.Text,
                PhoneNumberStaff = NumberPhoneTb.Text,
                IdPosition = Int32.Parse(PositionCb.SelectedValue.ToString()),
                IdDepartments = Int32.Parse(DepartmentsCb.SelectedValue.ToString()),
                PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName),
                IdStatusStaff = Int32.Parse(StatusStaffCb.SelectedValue.ToString()),
                IdUser = user.IdUser
            };
            DBEntities.GetContext().Staff.Add(staffAdd);
            DBEntities.GetContext().SaveChanges();
            staff.IdStaff = staffAdd.IdStaff;
        }
        private void AddPhoto()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = "";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Portable Network Graphic (*.png)|*.png";

                if (op.ShowDialog() == true)
                {
                    selectedFileName = op.FileName;
                    staff.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    PhotoIm.Source = ImageClass.ConvertByteArrayToImage(staff.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }

        }
        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkLogin = DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == LoginUserTb.Text);
            if (checkLogin != null)
            {
                MBClass.ErrorMB("Логин занят");
                PasswordUserTb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginUserTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginUserTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordUserTb.Text))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordUserTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(RoleCb.Text))
            {
                MBClass.ErrorMB("Выберите роль");
                RoleCb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(StatusUserCb.Text))
            {
                MBClass.ErrorMB("Выберите статус");
                StatusUserCb.Focus();
            }
            else
            {
                try
                {
                    UserAdd();
                    StaffAdd();
                    MBClass.InfoMB("Пользователя успешно добавлен");
                    if (VariableClass.ListUserPage1 != null) VariableClass.ListUserPage1.UpdateList();
                    if (VariableClass.ListUserStaffPage1 != null) VariableClass.ListUserStaffPage1.UpdateList();
                    if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.Update();
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                }
                if (VariableClass.ListUserStaffPage1 != null) VariableClass.ListUserStaffPage1.UpdateList();
                Close();
            }
        }

        private void LoadImBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        private void AddPositionBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddPositionWindow().Show();
        }

        private void AddDepBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddDepWindow().Show();
        }

        private void AddStStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStatusStaffWindow().Show();  
        }

        private void AddRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddRoleWindow().Show();
        }

        private void AddStUserBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStUserWindow().Show();
        }
    }
}
