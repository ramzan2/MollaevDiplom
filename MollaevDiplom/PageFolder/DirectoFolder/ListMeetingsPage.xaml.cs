﻿using System;
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

namespace MollaevDiplom.PageFolder.DirectoFolder
{
    /// <summary>
    /// Логика взаимодействия для ListMeetingsPage.xaml
    /// </summary>
    public partial class ListMeetingsPage : Page
    {
        public ListMeetingsPage()
        {
            Meetings meetings = new Meetings();
            InitializeComponent();
            VariableClass.ListMeetingsPage1 = this;
            UpdateList();
        }

        public void UpdateList()
        {
            ListMeetingsDT.ItemsSource = DBEntities.GetContext()
        .Meetings.Where(u => u.AgendaMeetigns
        .StartsWith(SearchBox.Text))
        .ToList().OrderBy(u => u.AgendaMeetigns);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddMtBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteIM_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
