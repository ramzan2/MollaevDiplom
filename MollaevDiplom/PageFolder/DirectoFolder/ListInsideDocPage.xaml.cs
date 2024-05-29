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
using System.Xml.Linq;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;
using MollaevDiplom.WindowFolder.DirectorWindowFolder;

namespace MollaevDiplom.PageFolder.DirectoFolder
{
    /// <summary>
    /// Логика взаимодействия для ListInsideDocPage.xaml
    /// </summary>
    public partial class ListInsideDocPage : Page
    {
        public ListInsideDocPage()
        {
            Documents documents = new Documents();
            InitializeComponent();
            SortCB.Text = "Формат";
            VariableClass.ListInsideDocPage1 = this;
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
        private void AddInDcBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddDocInWindow().Show();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {
            Documents documents = ListDocInDT.SelectedItem as Documents;

            if (ListDocInDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите документ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить " +
                    $"документ? {documents.NameDocuments} " +
                    $"{documents.NameDocuments} {documents.NameDocuments}?"))
                {
                    DBEntities.GetContext().Documents
                        .Remove(ListDocInDT.SelectedItem as Documents);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InfoMB("Документ удален");
                    ListDocInDT.ItemsSource = DBEntities.GetContext()
                        .Documents.ToList().OrderBy(r => r.NameDocuments);
                }

            }
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (ListDocInDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "документ для редактирования");
            }
            else
            {
                new EditDocInWindow(ListDocInDT.SelectedItem as Documents).Show();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void DocTb_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

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
