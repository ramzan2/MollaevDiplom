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
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.PageFolder.DirectoFolder
{
    /// <summary>
    /// Логика взаимодействия для InfoStaffPage.xaml
    /// </summary>
    public partial class InfoStaffPage : Page
    {
        Staff staff = new Staff();
        public InfoStaffPage(Staff staff)
        {
            InitializeComponent();

            DataContext = staff;

            this.staff.IdStaff = staff.IdStaff;

        }

        private void BakcBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListStaffPage());
        }
    }
}
