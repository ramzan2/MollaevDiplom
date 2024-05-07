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

namespace MollaevDiplom.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuDirectorWindow.xaml
    /// </summary>
    public partial class MenuDirectorWindow : Window
    {
        public MenuDirectorWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageFolder.DirectoFolder.ListStaffPage());
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Meetings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Document_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tasks_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Tasks_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void EditAccBtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthorizationWindow().Show();
            Close();
        }
    }
}
