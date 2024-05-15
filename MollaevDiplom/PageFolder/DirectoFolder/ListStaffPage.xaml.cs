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
    /// Логика взаимодействия для ListStaffPage.xaml
    /// </summary>
    public partial class ListStaffPage : Page
    {
        public ListStaffPage()
        {
            Staff staff = new Staff();
            InitializeComponent();
            VariableClass.ListStaffPage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListStaffLb.ItemsSource = DBEntities.GetContext()
          .Staff.Where(u => u.LastNameStaff
          .StartsWith(SearchBox.Text))
          .ToList().OrderBy(u => u.FirstNameStaff);

        }
        private void EditTE_Click(object sender, RoutedEventArgs e)
        {
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
            new EditStaffWindow(ListStaffLb.SelectedItem as Staff).ShowDialog();
        }

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStaffWindow().Show();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.DirectoFolder.InfoStaffPage(ListStaffLb.SelectedItem as Staff));
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void InfoTE_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.DirectoFolder.InfoStaffPage(ListStaffLb.SelectedItem as Staff));
        }
    }
}
