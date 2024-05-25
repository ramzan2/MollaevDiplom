using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;

namespace MollaevDiplom.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для EditIncomingDocWindow.xaml
    /// </summary>
    public partial class EditIncomingDocWindow : Window
    {
        IncomingDocuments originalIncomingDoc;
        Sender originalSender = new Sender();
        public EditIncomingDocWindow(IncomingDocuments incomingDocuments)
        {
            InitializeComponent();
            DBEntities.NullContext();
            originalIncomingDoc = DBEntities.GetContext().IncomingDocuments
                .FirstOrDefault(u => u.IdIncomingDocuments == incomingDocuments.IdIncomingDocuments);
            DataContext = originalIncomingDoc;
            this.originalIncomingDoc = incomingDocuments;
            CategoryCb.ItemsSource = DBEntities.GetContext()
                  .DocumentsCategory.ToList();
            LastNamePerformerCb.ItemsSource = DBEntities.GetContext()
                   .Staff.ToList();
            FirstNameStaffCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            MiddleNamePerformerCb.ItemsSource = DBEntities.GetContext()
           .Staff.ToList();
            IdMarkExecutionCb.ItemsSource = DBEntities.GetContext()
            .MarkExecution.ToList();
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
        private void EditSender()
         {
            try
            {
                originalSender = DBEntities.GetContext().Sender.FirstOrDefault(r => r.IdSender == originalSender.IdSender);
                originalSender.LastNameSender = LastNamePerformerCb.Text;
                originalSender.FirstNameSender = FirstNameSenderTb.Text;
                originalSender.MiddleNameSender = MiddleNamePerformerCb.Text;
                originalSender.NumberPhone = NumberPhoneTb.Text;
                originalSender.EmailSender = EmailSenderTb.Text;
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex.Message);
            }
        }

        private void EditIncomingDoc()
        {
            try
            {
                if(doc == null)
                {
                    originalIncomingDoc = DBEntities.GetContext().IncomingDocuments.FirstOrDefault(r => r.IdIncomingDocuments == originalIncomingDoc.IdIncomingDocuments);
                    originalIncomingDoc.NumberIncoming = Convert.ToInt32(NumberIncomingTb.Text);
                    originalIncomingDoc.DateIncoming = Convert.ToDateTime(DateIncomingDT.SelectedDate);
                    originalIncomingDoc.IdCategory = Convert.ToInt32(CategoryCb.SelectedValue);
                    originalIncomingDoc.IdSender = originalSender.IdSender;
                    originalIncomingDoc.NameIncoming = NameIncomingTb.Text;
                    originalIncomingDoc.SummaryIncoming = SummaryIncomingTb.Text;
                    originalIncomingDoc.IdStaff = Convert.ToInt32(LastNamePerformerCb.SelectedValue);
                    originalIncomingDoc.DateOfReceipt = Convert.ToDateTime(DateOfReceiptDT.SelectedDate);
                    originalIncomingDoc.OutgoingNumber = Convert.ToInt32(OutgoingNumberTb.Text);
                    originalIncomingDoc.DateOutgoingNumber = Convert.ToDateTime(DtDateOutgoingNumber.SelectedDate);
                    originalIncomingDoc.ControlDate = Convert.ToDateTime(DtControlDate.SelectedDate);
                    originalIncomingDoc.ExecutionDate = Convert.ToDateTime(ExecutionDateDT.SelectedDate);
                    originalIncomingDoc.IdMarkExecution = Convert.ToInt32(IdMarkExecutionCb.SelectedValue);
                    originalIncomingDoc.QuantityОfСopies = Convert.ToInt32(QuantityОfСopiesTb.Text);
                    originalIncomingDoc.QuantityPage = Convert.ToInt32(QuantityPageTb.Text);
                    DBEntities.GetContext().SaveChanges();
                }
                else
                {
                    originalIncomingDoc = DBEntities.GetContext().IncomingDocuments.FirstOrDefault(r => r.IdIncomingDocuments == originalIncomingDoc.IdIncomingDocuments);
                    originalIncomingDoc.NumberIncoming = Convert.ToInt32(NumberIncomingTb.Text);
                    originalIncomingDoc.DateIncoming = Convert.ToDateTime(DateIncomingDT.SelectedDate);
                    originalIncomingDoc.IdCategory = Convert.ToInt32(CategoryCb.SelectedValue);
                    originalIncomingDoc.FileDocuments = doc;
                    originalIncomingDoc.IdSender = originalSender.IdSender;
                    originalIncomingDoc.NameIncoming = NameIncomingTb.Text;
                    originalIncomingDoc.SummaryIncoming = SummaryIncomingTb.Text;
                    originalIncomingDoc.IdStaff = Convert.ToInt32(LastNamePerformerCb.SelectedValue);
                    originalIncomingDoc.DateOfReceipt = Convert.ToDateTime(DateOfReceiptDT.SelectedDate);
                    originalIncomingDoc.OutgoingNumber = Convert.ToInt32(OutgoingNumberTb.Text);
                    originalIncomingDoc.DateOutgoingNumber = Convert.ToDateTime(DtDateOutgoingNumber.SelectedDate);
                    originalIncomingDoc.ControlDate = Convert.ToDateTime(DtControlDate.SelectedDate);
                    originalIncomingDoc.ExecutionDate = Convert.ToDateTime(ExecutionDateDT.SelectedDate);
                    originalIncomingDoc.IdMarkExecution = Convert.ToInt32(IdMarkExecutionCb.SelectedValue);
                    originalIncomingDoc.QuantityОfСopies = Convert.ToInt32(QuantityОfСopiesTb.Text);
                    originalIncomingDoc.QuantityPage = Convert.ToInt32(QuantityPageTb.Text);
                    DBEntities.GetContext().SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex.Message);
            }
        }
        byte[] doc;
        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (doc != null)
            {
                EditSender();
                EditIncomingDoc();
                MBClass.InfoMB("Входящий документ добавлен");
                if (VariableClass.ListIncomingDocPage1 != null) VariableClass.ListIncomingDocPage1.UpdateList();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                Close();
                return;
            }
                EditSender();
                EditIncomingDoc();
                if (VariableClass.ListIncomingDocPage1 != null) VariableClass.ListIncomingDocPage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                     MBClass.InfoMB("Входящий документ добавлен");
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
            Close();  
        }

        private void LoadDcBtn_Click(object sender, RoutedEventArgs e)
        {
            doc = DocumentClass.ConvertDocumentToByteArray();
        }
    }
}
