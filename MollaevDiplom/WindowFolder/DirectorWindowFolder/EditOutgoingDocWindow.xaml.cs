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
    /// Логика взаимодействия для EditOutgoingDocWindow.xaml
    /// </summary>
    public partial class EditOutgoingDocWindow : Window
    {
        OutgoingDocuments originalOutgoingDocuments;
        public EditOutgoingDocWindow(OutgoingDocuments outgoingDocuments)
        {
            InitializeComponent();
            DBEntities.NullContext();
            originalOutgoingDocuments = DBEntities.GetContext().OutgoingDocuments
                .FirstOrDefault(u => u.IdOutgoingDocuments == outgoingDocuments.IdOutgoingDocuments);

            DataContext = originalOutgoingDocuments;


            CategoryCb.ItemsSource = DBEntities.GetContext()
                 .DocumentsCategory.ToList();
            LastNamePerformerCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
            FirstNameStaffCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MiddleNamePerformerCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            LastGrantorCb.ItemsSource = DBEntities.GetContext()
                  .Staff.ToList();
            FirGrantorCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MiddGrantorCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            IdMarkExecutionCb.ItemsSource = DBEntities.GetContext()
          .MarkExecution.ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void LoadDcBtn_Click(object sender, RoutedEventArgs e)
        {
            doc = DocumentClass.ConvertDocumentToByteArray();
        }
        byte[] doc;
        private void SaveDcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (doc == null)
            {
                MBClass.InfoMB("Данные успешно отредактированы");
                if (VariableClass.ListOutDocumentsPage1 != null) VariableClass.ListOutDocumentsPage1.UpdateList();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                DBEntities.GetContext().SaveChanges();
                Close();
                return;
            }
            originalOutgoingDocuments = DBEntities.GetContext().OutgoingDocuments
          .FirstOrDefault(u => u.IdOutgoingDocuments == originalOutgoingDocuments.IdOutgoingDocuments);
            originalOutgoingDocuments.NumberOutgoing = Convert.ToInt32(NumberOutgoingTb.Text);
            originalOutgoingDocuments.DateOutgoing = Convert.ToDateTime(DateOutgoing.SelectedDate);
            originalOutgoingDocuments.IdCategory = Convert.ToInt32(CategoryCb.SelectedValue);
            originalOutgoingDocuments.IdPerformer = Convert.ToInt32(LastNamePerformerCb.SelectedValue);
            originalOutgoingDocuments.NameOutgoingDocuments = NameDocTb.Text;
            originalOutgoingDocuments.SummaryOutgoing = SummaryOutgoingTb.Text;
            originalOutgoingDocuments.IdStaff = Convert.ToInt32(LastGrantorCb.SelectedValue);
            originalOutgoingDocuments.ControlDate = Convert.ToDateTime(DtControlDate.SelectedDate);
            originalOutgoingDocuments.ExecutionDate = Convert.ToDateTime(DtExecutionDate.SelectedDate);
            originalOutgoingDocuments.IdMarkExecution = Convert.ToInt32(IdMarkExecutionCb.SelectedValue);
            originalOutgoingDocuments.QuantityОfСopies = Int32.Parse(QuantityОfСopiesTb.Text);
            originalOutgoingDocuments.QuantityPage = Int32.Parse(QuantityPageTb.Text);
            originalOutgoingDocuments.FileDocuments = doc;
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Данные успешно отредактированы");
            if (VariableClass.ListOutDocumentsPage1 != null) VariableClass.ListOutDocumentsPage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
            Close();
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
