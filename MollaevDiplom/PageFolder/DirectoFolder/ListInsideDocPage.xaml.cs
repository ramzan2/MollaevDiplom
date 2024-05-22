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
    /// Логика взаимодействия для ListInsideDocPage.xaml
    /// </summary>
    public partial class ListInsideDocPage : Page
    {
        public ListInsideDocPage()
        {
            Documents documents = new Documents();
            InitializeComponent();
            VariableClass.ListInsideDocPage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListDocInDT.ItemsSource = DBEntities.GetContext()
        .Documents.Where(u => u.NameDocuments
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.NameDocuments);
        }
        private void AddInDcBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddDocInWindow().Show();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
