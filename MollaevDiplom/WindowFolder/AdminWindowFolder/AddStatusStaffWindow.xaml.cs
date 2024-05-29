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

namespace MollaevDiplom.WindowFolder.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStatusStaffWindow.xaml
    /// </summary>
    public partial class AddStatusStaffWindow : Window
    {
        public AddStatusStaffWindow()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddPsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PositionTb.Text))
            {
                MBClass.ErrorMB("Заполните поле");
            }
            else
            {
                DBEntities.GetContext().StatusStaff.Add(new StatusStaff()
                {
                    NameStatusStaff = PositionTb.Text
                });
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Статус успешно добавлен");
                Close();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
