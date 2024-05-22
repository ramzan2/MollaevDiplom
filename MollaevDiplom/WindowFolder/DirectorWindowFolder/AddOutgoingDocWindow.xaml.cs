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
    /// Логика взаимодействия для AddOutgoingDocWindow.xaml
    /// </summary>
    public partial class AddOutgoingDocWindow : Window
    {
        OutgoingDocuments outgoingDocuments = new OutgoingDocuments();
        public AddOutgoingDocWindow()
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
            Close();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddDcBtn_Click(object sender, RoutedEventArgs e)
        {
            var doc = DocumentClass.ConvertDocumentToByteArray();
            DBEntities.GetContext().OutgoingDocuments.Add(new OutgoingDocuments()
            {
                NumberOutgoing = Convert.ToInt32(NumberOutgoingTb.Text),
                DateOutgoing = Convert.ToDateTime(DateOutgoing.SelectedDate),
                IdCategory = Convert.ToInt32(CategoryCb.SelectedValue),
                IdPerformer = Convert.ToInt32(LastGrantorCb.SelectedValue),
                NameOutgoingDocuments = NameDocTb.Text,
                SummaryOutgoing = SummaryOutgoingTb.Text,
                IdStaff = Convert.ToInt32(LastNamePerformerCb.SelectedValue),
                ControlDate = Convert.ToDateTime(DtControlDate.SelectedDate),
                ExecutionDate = Convert.ToDateTime(DtExecutionDate.SelectedDate),
                IdMarkExecution = Convert.ToInt32(IdMarkExecutionCb.SelectedValue),
                QuantityОfСopies = Convert.ToInt32(QuantityОfСopiesTb.Text),
                QuantityPage = Convert.ToInt32(QuantityPageTb.Text),
                FileDocuments = doc
            });
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Документ успешно добавлен");
            if (VariableClass.ListOutDocumentsPage1 != null) VariableClass.ListOutDocumentsPage1.UpdateList();
            if (VariableClass.direcWindow != null) VariableClass.direcWindow.Update();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.Update();
            Close();
        }

        private void LoadDcBtn_Click(object sender, RoutedEventArgs e)
        {
            var doc = DocumentClass.ConvertDocumentToByteArray();
        }
    }
}
