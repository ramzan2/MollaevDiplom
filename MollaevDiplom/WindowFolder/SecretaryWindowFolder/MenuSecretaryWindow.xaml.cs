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

namespace MollaevDiplom.WindowFolder.SecretaryWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuSecretaryWindow.xaml
    /// </summary>
    public partial class MenuSecretaryWindow : Window
    {
        public MenuSecretaryWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListMtSecPage());
            SidePanel.Visibility = Visibility.Visible;
            VariableClass.MenuSecretaryWindow1 = this;
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
        private void Meetings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListMtSecPage());
            SidePanel.Visibility = Visibility.Visible;
        }

        private void InsideDoc_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListDocInSecPage());
            SidePanel.Visibility = Visibility.Collapsed;
        }

        private void DocumentIncoming_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListInDocSecPage());
            SidePanel.Visibility = Visibility.Collapsed;
        }

        private void DocumentOutgoing_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListOutDocSecPage());
            SidePanel.Visibility = Visibility.Collapsed;
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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

        private void Tg_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AttendanceBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListAttSecPage());
        }

        private void MeetingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFolder.SecretaryFolder.ListMtSecPage());
        }
    }
}
