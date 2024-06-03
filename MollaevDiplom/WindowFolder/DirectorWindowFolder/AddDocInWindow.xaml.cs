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
using System.Xml.Linq;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddDocInWindow.xaml
    /// </summary>
    public partial class AddDocInWindow : Window
    {
        Documents documents = new Documents();
        public AddDocInWindow()
        {
            InitializeComponent();
            CategoryCb.ItemsSource = DBEntities.GetContext()
                  .DocumentsCategory.ToList();
            LastNameStaffCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
            FirstNameStaffCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MiddleNameStaffCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MarkExecutionCb.ItemsSource = DBEntities.GetContext()
          .MarkExecution.ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if(VariableClass.direcWindow != null)VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
        }

        private void LoadDcBtn_Click(object sender, RoutedEventArgs e)
        {
          doc = DocumentClass.ConvertDocumentToByteArray();
        }
        byte[] doc;
        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleDocumentsTb.Text) ||
           string.IsNullOrWhiteSpace(DescriptionDocumentsTb.Text) ||
           string.IsNullOrWhiteSpace(CategoryCb.Text) ||
           string.IsNullOrWhiteSpace(LastNameStaffCb.Text) ||
           string.IsNullOrWhiteSpace(DtUpload.Text) ||
           string.IsNullOrWhiteSpace(QuantityPageTb.Text) ||
           string.IsNullOrWhiteSpace(QuantityOfCopiesTb.Text) ||
           string.IsNullOrWhiteSpace(DtDateOfExecution.Text) ||
           string.IsNullOrWhiteSpace(NameDocTb.Text))
            {
                MBClass.ErrorMB("Заполните все поля");
            }
           else
            {
                if (doc == null)
                {
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMB("Документ успешно добавлен");
                    if (VariableClass.ListInsideDocPage1 != null) VariableClass.ListInsideDocPage1.UpdateList();
                    if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                    if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                    Close();
                }
                DBEntities.GetContext().Documents.Add(new Documents()
                {
                    TitleDocuments = TitleDocumentsTb.Text,
                    DescriptionDocuments = DescriptionDocumentsTb.Text,
                    IdCategory = Convert.ToInt32(CategoryCb.SelectedValue),
                    IdStaff = Convert.ToInt32(LastNameStaffCb.SelectedValue),
                    UploadDate = Convert.ToDateTime(DtUpload.SelectedDate),
                    FileDocuments = doc,
                    QuantityPage = Convert.ToInt32(QuantityPageTb.Text),
                    QuantityОfСopies = Convert.ToInt32(QuantityOfCopiesTb.Text),
                    DateOfExecution = Convert.ToDateTime(DtDateOfExecution.SelectedDate),
                    NameDocuments = NameDocTb.Text,
                    IdMarkExecution = Convert.ToInt32(MarkExecutionCb.SelectedValue)
                });
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Документ успешно добавлен");
                if (VariableClass.ListInsideDocPage1 != null) VariableClass.ListInsideDocPage1.UpdateList();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                Close();
            }
        }

        private void AddTypeDocBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddTypeDocWindow().Show();
        }

        private void AddMarkDocBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddMarkExWindow().Show();
        }
    }
}
