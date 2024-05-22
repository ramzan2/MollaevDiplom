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
using Microsoft.Win32;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStaffWindow.xaml
    /// </summary>
    public partial class AddStaffWindow : Window
    {
        Staff staff = new Staff();
        public AddStaffWindow()
        {
            InitializeComponent();
            PositionCb.ItemsSource = DBEntities.GetContext()
                   .Position.ToList();
            DepartmentsCb.ItemsSource = DBEntities.GetContext()
                   .Departments.ToList();
                    StatusCb.ItemsSource = DBEntities.GetContext()
                   .StatusStaff.ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
        }

        private void LoadImBtn_Click(object sender, RoutedEventArgs e)
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
                    staff.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    PhotoIm.Source = ImageClass.ConvertByteArrayToImage(staff.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            DBEntities.GetContext().Staff.Add(new Staff()
            {
                LastNameStaff = LastNameTb.Text,
                FirstNameStaff = FirstNameTb.Text,
                MiddleNameStaff = MiddleNameTb.Text,
                PhoneNumberStaff = PhoneNumberStaffTb.Text,
                IdPosition = Int32.Parse(PositionCb.SelectedValue.ToString()),
                IdDepartments = Int32.Parse(DepartmentsCb.SelectedValue.ToString()),
                PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName),
                IdStatusStaff = Int32.Parse(StatusCb.SelectedValue.ToString())
            });
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Сотрудник успешно добавлен");
            if (VariableClass.ListStaffPage1 != null) VariableClass.ListStaffPage1.UpdateList();
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
