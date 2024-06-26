﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;
using MollaevDiplom.WindowFolder.DirectorWindowFolder;

namespace MollaevDiplom.PageFolder.DirectoFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOutDocumentsPage.xaml
    /// </summary>
    public partial class ListOutDocumentsPage : Page
    {
        public ListOutDocumentsPage()
        {
            OutgoingDocuments outgoingDocuments = new OutgoingDocuments();
            InitializeComponent();
            SortCB.Text = "Формат";
            VariableClass.ListOutDocumentsPage1 = this;
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
        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (ListDocInDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "документ для редактирования");
            }
            else
            {
                new EditOutgoingDocWindow(ListDocInDT.SelectedItem as OutgoingDocuments).Show();
                if (VariableClass.direcWindow != null) VariableClass.direcWindow.UpdateList();
                if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
            }

        }
        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {
            OutgoingDocuments outgoingDocuments = ListDocInDT.SelectedItem as OutgoingDocuments;

            if (ListDocInDT.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите документ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QuestionMB("Удалить " +
                    $"документ? {outgoingDocuments.NameOutgoingDocuments} " +
                    $"{outgoingDocuments.NameOutgoingDocuments} {outgoingDocuments.NameOutgoingDocuments}?"))
                {
                    DBEntities.GetContext().OutgoingDocuments
                        .Remove(ListDocInDT.SelectedItem as OutgoingDocuments);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InfoMB("Документ удален");
                    ListDocInDT.ItemsSource = DBEntities.GetContext()
                        .OutgoingDocuments.ToList().OrderBy(r => r.NameOutgoingDocuments);
                }

            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void AddOutDcBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddOutgoingDocWindow().Show();
            if(VariableClass.direcWindow != null)VariableClass.direcWindow.UpdateList();
            if (VariableClass.MenuSecretaryWindow1 != null) VariableClass.MenuSecretaryWindow1.UpdateList();
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
