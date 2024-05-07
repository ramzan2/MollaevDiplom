using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MollaevDiplom.ClassFolder;

namespace MollaevDiplom.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuDirecWindow.xaml
    /// </summary>
    public partial class MenuDirecWindow : Window
    {
        public MenuDirecWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageFolder.DirectoFolder.ListStaffPage());
            VariableClass.direcWindow = this;
            UpdateList();
            Update();
        }

        public void UpdateList()
        {
            BigGrid.Opacity = 0.2;
        }

        public void Update()
        {
            BigGrid.Opacity = 1.86;
        }
        private void Tasks_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            SidePanel.Visibility = Visibility.Collapsed;
        }

        private void Tasks_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Meetings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.DirectoFolder.ListMeetingsPage());
            SidePanel.Visibility = Visibility.Visible;
        }

        private void Document_Click(object sender, RoutedEventArgs e)
        {
            SidePanel.Visibility = Visibility.Collapsed;
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.DirectoFolder.ListStaffPage());
            SidePanel.Visibility = Visibility.Collapsed;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void EditAccBtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthorizationWindow().Show();
            Close();
        }

        private void MeetingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.DirectoFolder.ListMeetingsPage());
        }

        private void AttendanceBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.DirectoFolder.ListAttendancePage());
        }
    }
}
