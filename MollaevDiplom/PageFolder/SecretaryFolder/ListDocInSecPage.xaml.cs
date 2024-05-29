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
    /// Логика взаимодействия для ListDocInSecPage.xaml
    /// </summary>
    public partial class ListDocInSecPage : Page
    {
        public ListDocInSecPage()
        {
            Documents documents = new Documents();
            InitializeComponent();
            SortCB.Text = "Формат";
            UpdateList();
        }
        public void UpdateList()
        {
            ListDocInDT.ItemsSource = DBEntities.GetContext()
        .Documents.Where(u => u.NameDocuments
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.NameDocuments);
            IQueryable<Documents> sortList = DBEntities.GetContext().Documents;

            SortselectedCombobox(ref sortList);

            sortList = sortList.Where(u => u.TitleDocuments.Contains(SearchBox.Text)
                                        || u.DescriptionDocuments.Contains(SearchBox.Text)
                                        || u.DocumentsCategory.NameCategory.Contains(SearchBox.Text)
                                        || u.Staff.LastNameStaff.Contains(SearchBox.Text)
                                        || u.NameDocuments.Contains(SearchBox.Text));

            sortList = sortList.OrderByDescending(u => u.IdDocuments);
            ListDocInDT.ItemsSource = sortList.ToList();
        }
        private void SortselectedCombobox(ref IQueryable<Documents> sortList)
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

        private void DocBtn_Click(object sender, RoutedEventArgs e)
        {
            Documents documents1 = ListDocInDT.SelectedItem as Documents;
            MBClass.InfoMB(documents1.NameDocuments);
            DocumentClass.ConvertByteArrayToDocument(documents1.FileDocuments);
        }

        private async void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(1);
            UpdateList();
        }
    }
}
