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
    /// Логика взаимодействия для ListIncomingDocPage.xaml
    /// </summary>
    public partial class ListIncomingDocPage : Page
    {
        public ListIncomingDocPage()
        {
            IncomingDocuments incomingDocuments = new IncomingDocuments();
            InitializeComponent();
            VariableClass.ListIncomingDocPage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListDocInDT.ItemsSource = DBEntities.GetContext()
        .IncomingDocuments.Where(u => u.NameIncoming
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.NameIncoming);
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void AddInDcBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddIncomingDocWindow().Show();
            if(VariableClass.direcWindow != null)VariableClass.direcWindow.UpdateList();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {
            IncomingDocuments incomingDocuments = ListDocInDT.SelectedItem as IncomingDocuments;

            if (ListDocInDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите документ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить " +
                    $"документ? {incomingDocuments.NameIncoming} " +
                    $"{incomingDocuments.NameIncoming} {incomingDocuments.NameIncoming}?"))
                {
                    DBEntities.GetContext().IncomingDocuments
                        .Remove(ListDocInDT.SelectedItem as IncomingDocuments);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InfoMB("Документ удален");
                    ListDocInDT.ItemsSource = DBEntities.GetContext()
                        .IncomingDocuments.ToList().OrderBy(r => r.NameIncoming);
                }

            }
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (ListDocInDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "пользователя для редактирования");
            }
            else
            {
                new EditIncomingDocWindow(ListDocInDT.SelectedItem as IncomingDocuments).Show();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
            }
        }

        private void DocIM_Click(object sender, RoutedEventArgs e)
        {
            IncomingDocuments documents1 = ListDocInDT.SelectedItem as IncomingDocuments;
            MBClass.InfoMB(documents1.NameIncoming);
            DocumentClass.ConvertByteArrayToDocument(documents1.FileDocuments);
        }
    }
}
