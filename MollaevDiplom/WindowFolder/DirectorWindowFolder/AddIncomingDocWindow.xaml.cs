using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddIncomingDocWindow.xaml
    /// </summary>
    public partial class AddIncomingDocWindow : Window
    {
        IncomingDocuments incomingDocuments = new IncomingDocuments();
        Sender sender1 = new Sender();
        public AddIncomingDocWindow()
        {
            InitializeComponent();
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

        private void SenderAdd()
        {
            var senderAdd = new Sender()
            {
                LastNameSender = LastNameSenderTb.Text,
                FirstNameSender = FirstNameSenderTb.Text,
                MiddleNameSender = MiddleNameSenderTb.Text,
                NumberPhone = NumberPhoneTb.Text,
                EmailSender = EmailSenderTb.Text
            };
            DBEntities.GetContext().Sender.Add(senderAdd);
            DBEntities.GetContext().SaveChanges();
            sender1.IdSender = senderAdd.IdSender;
        }
        
        private void OutgoingDocumentAdd()
        {
            if (string.IsNullOrWhiteSpace(NumberIncomingTb.Text) ||
          string.IsNullOrWhiteSpace(DateIncomingDT.Text) ||
          string.IsNullOrWhiteSpace(CategoryCb.Text) ||
          string.IsNullOrWhiteSpace(LastNameSenderTb.Text) ||
          string.IsNullOrWhiteSpace(FirstNameSenderTb.Text) ||
          string.IsNullOrWhiteSpace(MiddleNameSenderTb.Text) ||
          string.IsNullOrWhiteSpace(NumberPhoneTb.Text) ||
          string.IsNullOrWhiteSpace(EmailSenderTb.Text) ||
          string.IsNullOrWhiteSpace(NameIncomingTb.Text) ||
          string.IsNullOrWhiteSpace(SummaryIncomingTb.Text) ||
          string.IsNullOrWhiteSpace(LastNamePerformerCb.Text) ||
          string.IsNullOrWhiteSpace(DateOfReceiptDT.Text) ||
          string.IsNullOrWhiteSpace(OutgoingNumberTb.Text) ||
          string.IsNullOrWhiteSpace(DtDateOutgoingNumber.Text) ||
          string.IsNullOrWhiteSpace(DtControlDate.Text) ||
          string.IsNullOrWhiteSpace(ExecutionDateDT.Text) ||
          string.IsNullOrWhiteSpace(IdMarkExecutionCb.Text) ||
          string.IsNullOrWhiteSpace(QuantityОfСopiesTb.Text) ||
          string.IsNullOrWhiteSpace(QuantityPageTb.Text))
            {
                MBClass.ErrorMB("Заполните все поля");
            }
            else
            {
                var incomingDocumentAdd = new IncomingDocuments()
                {
                    NumberIncoming = Convert.ToInt32(NumberIncomingTb.Text),
                    DateIncoming = Convert.ToDateTime(DateIncomingDT.SelectedDate),
                    IdCategory = Convert.ToInt32(CategoryCb.SelectedValue),
                    FileDocuments = doc,
                    IdSender = sender1.IdSender,
                    NameIncoming = NameIncomingTb.Text,
                    SummaryIncoming = SummaryIncomingTb.Text,
                    IdStaff = Convert.ToInt32(LastNamePerformerCb.SelectedValue),
                    DateOfReceipt = Convert.ToDateTime(DateOfReceiptDT.SelectedDate),
                    OutgoingNumber = Convert.ToInt32(OutgoingNumberTb.Text),
                    DateOutgoingNumber = Convert.ToDateTime(DtDateOutgoingNumber.SelectedDate),
                    ControlDate = Convert.ToDateTime(DtControlDate.SelectedDate),
                    ExecutionDate = Convert.ToDateTime(ExecutionDateDT.SelectedDate),
                    IdMarkExecution = Convert.ToInt32(IdMarkExecutionCb.SelectedValue),
                    QuantityОfСopies = Convert.ToInt32(QuantityОfСopiesTb.Text),
                    QuantityPage = Convert.ToInt32(QuantityPageTb.Text),
                };
                DBEntities.GetContext().IncomingDocuments.Add(incomingDocumentAdd);
                DBEntities.GetContext().SaveChanges();
                incomingDocuments.IdIncomingDocuments = incomingDocumentAdd.IdIncomingDocuments;
            }
        }
        byte[] doc;
        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NumberIncomingTb.Text) ||
          string.IsNullOrWhiteSpace(DateIncomingDT.Text) ||
          string.IsNullOrWhiteSpace(CategoryCb.Text) ||
          string.IsNullOrWhiteSpace(LastNameSenderTb.Text) ||
          string.IsNullOrWhiteSpace(FirstNameSenderTb.Text) ||
          string.IsNullOrWhiteSpace(MiddleNameSenderTb.Text) ||
          string.IsNullOrWhiteSpace(NumberPhoneTb.Text) ||
          string.IsNullOrWhiteSpace(EmailSenderTb.Text) ||
          string.IsNullOrWhiteSpace(NameIncomingTb.Text) ||
          string.IsNullOrWhiteSpace(SummaryIncomingTb.Text) ||
          string.IsNullOrWhiteSpace(LastNamePerformerCb.Text) ||
          string.IsNullOrWhiteSpace(DateOfReceiptDT.Text) ||
          string.IsNullOrWhiteSpace(OutgoingNumberTb.Text) ||
          string.IsNullOrWhiteSpace(DtDateOutgoingNumber.Text) ||
          string.IsNullOrWhiteSpace(DtControlDate.Text) ||
          string.IsNullOrWhiteSpace(ExecutionDateDT.Text) ||
          string.IsNullOrWhiteSpace(IdMarkExecutionCb.Text) ||
          string.IsNullOrWhiteSpace(QuantityОfСopiesTb.Text) ||
          string.IsNullOrWhiteSpace(QuantityPageTb.Text))
            {
                MBClass.ErrorMB("Заполните все поля");
            }
            else
            {
                if (doc == null)
                {
                    SenderAdd();
                    OutgoingDocumentAdd();
                    MBClass.InfoMB("Входящий документ добавлен");
                    if (VariableClass.ListIncomingDocPage1 != null) VariableClass.ListIncomingDocPage1.UpdateList();
                    if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                    if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                    Close();
                }
                try
                {
                    SenderAdd();
                    OutgoingDocumentAdd();
                    MBClass.InfoMB("Входящий документ добавлен");
                    if (VariableClass.ListIncomingDocPage1 != null) VariableClass.ListIncomingDocPage1.UpdateList();
                    if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
                    if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
                    Close();
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void LoadDcBtn_Click(object sender, RoutedEventArgs e)
        {
             doc = DocumentClass.ConvertDocumentToByteArray();
        }
    }
}
