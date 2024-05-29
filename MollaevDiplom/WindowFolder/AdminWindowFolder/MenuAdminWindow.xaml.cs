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
using MollaevDiplom.PageFolder.AdminFolder;

namespace MollaevDiplom.WindowFolder.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuAdminWindow.xaml
    /// </summary>
    public partial class MenuAdminWindow : Window
    {
        public MenuAdminWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ListUsersPage());
            VariableClass.menuAdminWindow = this;
            UpdateList();
            Update();
        }

        public void UpdateList()
        {
            BigGrid.Opacity = 0.3;
        }

        public void Update()
        {
            BigGrid.Opacity = 1.86;
        }
        private void EditAccBtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthorizationWindow().Show();
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Tg_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListUsers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListUsersPage());
        }

        private void ListStaff_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListUserStaffPage());
        }
    }
}
