using System;
using System.Collections;
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
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.PageFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для InfoUserPage.xaml
    /// </summary>
    public partial class InfoUserPage : Page
    {
        private Staff originalStaff;
        public InfoUserPage(Staff existingStaff)
        {
            InitializeComponent();
            DataContext = existingStaff;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListUserStaffPage());
        }
    }
}
