using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.PageFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();
            ChatListBox.ItemsSource = DBEntities.GetContext()
            .Messages.ToList().OrderBy(u => u.IdMessages);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string messageText = SearchBox.Text;
            if (!string.IsNullOrWhiteSpace(messageText))
            {
                DBEntities.GetContext().Messages.Add(new Messages()
                {
                    ContentMessages = SearchBox.Text
                });
                DBEntities.GetContext().SaveChanges();
                ChatListBox.ItemsSource = DBEntities.GetContext()
              .Messages.ToList().OrderBy(u => u.IdMessages);
                //ChatListBox.ItemsSource = DBEntities.GetContext().
                //    Messages.ToList();
                SearchBox.Clear();
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (SearchBox.IsEnabled)
                {
                    SendButton_Click(sender, e);
                    return;
                }


                if (SearchBox.IsFocused)
                {
                    SearchBox.Focus();
                }
                else
                {
                    SearchBox.Focus();
                }
            }
        }
    }
}
