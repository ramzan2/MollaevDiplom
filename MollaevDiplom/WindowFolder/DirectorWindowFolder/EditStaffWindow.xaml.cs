using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для EditStaffWindow.xaml
    /// </summary>
    public partial class EditStaffWindow : Window
    {
        private Staff originalStaff;

        public EditStaffWindow(Staff existingStaff)
        {
            InitializeComponent();

            DBEntities.NullContext();
            originalStaff = DBEntities.GetContext().Staff
                .FirstOrDefault(u => u.IdStaff == existingStaff.IdStaff); 
            //editedStaff = CloneStaff(existingStaff);
            DataContext = originalStaff;
            if (originalStaff != null) selectedFileName = "фото есть";



            PositionCb.ItemsSource = DBEntities.GetContext()
                       .Position.ToList();
            DepartmentsCb.ItemsSource = DBEntities.GetContext()
                   .Departments.ToList();

            StatusStaffCb.ItemsSource = DBEntities.GetContext()
                   .StatusStaff.ToList();
            //this.staff.IdStaff = staff.IdStaff;


        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {   

                originalStaff = DBEntities.GetContext().Staff
                    .FirstOrDefault(u => u.IdStaff == originalStaff.IdStaff);
            if (selectedFileName == "фото есть")
            {
                originalStaff.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
            }
            originalStaff.LastNameStaff = FIOTb.Text;
            originalStaff.FirstNameStaff = FIOTb.Text;
            originalStaff.MiddleNameStaff = FIOTb.Text;
                originalStaff.PhoneNumberStaff = PhoneNumberStaffTb.Text;
                originalStaff.IdPosition = Int32.Parse(PositionCb.SelectedValue.ToString());
                originalStaff.IdDepartments = Int32.Parse(DepartmentsCb.SelectedValue.ToString());
            originalStaff.IdStatusStaff = Int32.Parse(StatusStaffCb.SelectedValue.ToString());
            DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Данные успешно отредактированы");
                if (VariableClass.ListStaffPage1 != null) VariableClass.ListStaffPage1.UpdateList();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                Close();
         
        }

        //private Staff CloneStaff(Staff staff)
        //{
        //    return new Staff
        //    {
        //        IdStaff = staff.IdStaff,
        //        FIOStaff = staff.FIOStaff,
        //        PhoneNumberStaff = staff.PhoneNumberStaff,
        //        IdPosition = staff.IdPosition,
        //        IdDepartments = staff.IdDepartments,
        //        PhotoStaff = staff.PhotoStaff

        //    };
        //}

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
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PositionCb.SelectedIndex =
             originalStaff.IdPosition;
            DepartmentsCb.SelectedIndex =
              originalStaff
              .IdDepartments;
        }
    }
}
