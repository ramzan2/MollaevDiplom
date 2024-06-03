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
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для EditDocInWindow.xaml
    /// </summary>
    public partial class EditDocInWindow : Window
    {
        Documents originalDocuments;
        public EditDocInWindow(Documents existingDocuments)
        {
            InitializeComponent();
            DBEntities.NullContext();
            originalDocuments = DBEntities.GetContext().Documents
                .FirstOrDefault(u => u.IdDocuments == existingDocuments.IdDocuments);
            DataContext = originalDocuments;
            this.originalDocuments.IdDocuments = originalDocuments.IdDocuments;

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
        byte[] doc;
        private void SaveDcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (doc == null)
            {
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Данные успешно отредактированы");
                if (VariableClass.ListInsideDocPage1 != null) VariableClass.ListInsideDocPage1.UpdateList();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                DBEntities.GetContext().SaveChanges();
                Close();
                return;
            }
            originalDocuments = DBEntities.GetContext().Documents
                    .FirstOrDefault(u => u.IdDocuments == originalDocuments.IdDocuments);
            originalDocuments.TitleDocuments = TitleDocumentsTb.Text;
            originalDocuments.DescriptionDocuments = DescriptionDocumentsTb.Text;
            originalDocuments.IdCategory = Convert.ToInt32(CategoryCb.SelectedValue);
            originalDocuments.IdStaff = Convert.ToInt32(LastNameStaffCb.SelectedValue);
            originalDocuments.UploadDate = Convert.ToDateTime(DtUpload.SelectedDate);
            originalDocuments.FileDocuments = doc;
            originalDocuments.QuantityPage = Int32.Parse(QuantityPageTb.Text);
            originalDocuments.QuantityОfСopies = Int32.Parse(QuantityOfCopiesTb.Text);
            originalDocuments.DateOfExecution = Convert.ToDateTime(DtDateOfExecution.SelectedDate);
            originalDocuments.NameDocuments = NameDocTb.Text;
            originalDocuments.IdMarkExecution = Convert.ToInt32(MarkExecutionCb.SelectedValue); 
            DBEntities.GetContext().SaveChanges();
            if (VariableClass.ListInsideDocPage1 != null) VariableClass.ListInsideDocPage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
            Close();
        }

        private void LoadDcBtn_Click(object sender, RoutedEventArgs e)
        {
            doc = DocumentClass.ConvertDocumentToByteArray();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddMarkDocBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddMarkExWindow().Show();
        }

        private void AddTypeDocBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddTypeDocWindow().Show();
        }
    }
}
