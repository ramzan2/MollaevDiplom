using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        Staff originalStaff ;
        User originalUser;
        public EditUserWindow(Staff existingStaff)
        {
            InitializeComponent();

            DBEntities.NullContext();
            originalStaff = DBEntities.GetContext().Staff
                .FirstOrDefault(u => u.IdStaff == existingStaff.IdStaff);
            DataContext = originalStaff;
            this.originalStaff = existingStaff;
            this.originalUser = existingStaff.User;
            if (originalStaff != null) selectedFileName = "фото есть";

            RoleCb.ItemsSource = DBEntities.GetContext()
                .Role.ToList();
            StatusUserCb.ItemsSource = DBEntities.GetContext()
                   .StatusUser.ToList();
            StatusStaffCb.ItemsSource = DBEntities.GetContext()
                   .StatusStaff.ToList();
            PositionCb.ItemsSource = DBEntities.GetContext()
               .Position.ToList();
            DepartmentsCb.ItemsSource = DBEntities.GetContext()
              .Departments.ToList();
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
        public void EditUser()
        {
            try
            {
                originalUser = DBEntities.GetContext().User.FirstOrDefault(r => r.IdUser == originalUser.IdUser);
                originalUser.LoginUser = LoginUserTb.Text;
                originalUser.PasswordUser = PasswordUserTb.Text;
                originalUser.IdRole = Convert.ToInt32(RoleCb.SelectedValue);
                originalUser.IdStatusUser = Convert.ToInt32(StatusUserCb.SelectedValue);
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex.Message);
            }
        }
        public void EditStaff()
        {
            try
            {
                if (selectedFileName == "")
                {
                    originalStaff = DBEntities.GetContext().Staff
                                       .FirstOrDefault(u => u.IdStaff == originalStaff.IdStaff);
                    originalStaff.LastNameStaff = LastNameTb.Text;
                    originalStaff.FirstNameStaff = FirstNameTb.Text;
                    originalStaff.MiddleNameStaff = MiddleNameTb.Text;
                    originalStaff.PhoneNumberStaff = NumberPhoneTb.Text;
                    originalStaff.IdPosition = Int32.Parse(PositionCb.SelectedValue.ToString());
                    originalStaff.IdDepartments = Int32.Parse(DepartmentsCb.SelectedValue.ToString());
                    originalStaff.IdStatusStaff = Int32.Parse(StatusStaffCb.SelectedValue.ToString());
                    originalStaff.IdUser = originalUser.IdUser;
                    DBEntities.GetContext().SaveChanges();
                }
                else
                {
                    originalStaff = DBEntities.GetContext().Staff
                                       .FirstOrDefault(u => u.IdStaff == originalStaff.IdStaff);
                    originalStaff.LastNameStaff = LastNameTb.Text;
                    originalStaff.FirstNameStaff = FirstNameTb.Text;
                    originalStaff.MiddleNameStaff = MiddleNameTb.Text;
                    originalStaff.PhoneNumberStaff = NumberPhoneTb.Text;
                    originalStaff.IdPosition = Convert.ToInt32(PositionCb.SelectedValue);
                    originalStaff.IdDepartments = Convert.ToInt32(DepartmentsCb.SelectedValue);
                    if (selectedFileName != "фото есть")
                        originalStaff.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    originalStaff.IdStatusStaff = Convert.ToInt32(StatusStaffCb.SelectedValue);
                    originalStaff.IdUser = originalUser.IdUser;
                    DBEntities.GetContext().SaveChanges();
                }
                MBClass.InfoMB("Пользователь изменен");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex.Message);
            }
        }
        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            EditUser();
            EditStaff();
            if (VariableClass.ListUserPage1 != null) VariableClass.ListUserPage1.UpdateList();
            if (VariableClass.menuAdminWindow != null) VariableClass.menuAdminWindow.Update();
            Close();
        }
        private void EditImBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        string selectedFileName = "";
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
                    originalStaff.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    PhotoIm.Source = ImageClass.ConvertByteArrayToImage(originalStaff.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
        private void LoadImBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        private void EditStUserBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStUserWindow().Show();
        }

        private void EditDepBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddDepWindow().Show();
        }

        private void EditStStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStatusStaffWindow().Show();
        }

        private void EditRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddRoleWindow().Show();
        }

        private void EditPositionBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddPositionWindow().Show();
        }
    }
}
