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

namespace MollaevDiplom.PageFolder.SecretaryFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOutDocSecPage.xaml
    /// </summary>
    public partial class ListOutDocSecPage : Page
    {
        public ListOutDocSecPage()
        {
            OutgoingDocuments outgoingDocuments = new OutgoingDocuments();
            InitializeComponent();
            SortCB.Text = "Формат";
            UpdateList();
        }
        public void UpdateList()
        {
            ListDocInDT.ItemsSource = DBEntities.GetContext()
        .OutgoingDocuments.Where(u => u.NameOutgoingDocuments
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.NameOutgoingDocuments);
            IQueryable<OutgoingDocuments> sortList = DBEntities.GetContext().OutgoingDocuments;

            SortselectedCombobox(ref sortList);

            sortList = sortList.Where(u => u.DocumentsCategory.NameCategory.Contains(SearchBox.Text)
                                        || u.NameOutgoingDocuments.Contains(SearchBox.Text)
                                        || u.SummaryOutgoing.Contains(SearchBox.Text)
                                        || u.Staff.LastNameStaff.Contains(SearchBox.Text)
                                        || u.MarkExecution.NameMarkExecution.Contains(SearchBox.Text));

            sortList = sortList.OrderByDescending(u => u.IdOutgoingDocuments);
            ListDocInDT.ItemsSource = sortList.ToList();
        }
        private void SortselectedCombobox(ref IQueryable<OutgoingDocuments> sortList)
        {

            switch (SortCB.Text)
            {
                case "docx":
                    sortList = sortList.Where(u => u.IdCategory == 1);
                    break;
                case "PDF":
                    sortList = sortList.Where(u => u.IdCategory == 2);
                    break;
                case "doc":
                    sortList = sortList.Where(u => u.IdCategory == 3);
                    break;
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void LoadDocIM_Click(object sender, RoutedEventArgs e)
        {
            OutgoingDocuments documents1 = ListDocInDT.SelectedItem as OutgoingDocuments;
            MBClass.InfoMB(documents1.NameOutgoingDocuments);
            DocumentClass.ConvertByteArrayToDocument(documents1.FileDocuments);
        }

        private async void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(1);
            UpdateList();
        }
    }
}
