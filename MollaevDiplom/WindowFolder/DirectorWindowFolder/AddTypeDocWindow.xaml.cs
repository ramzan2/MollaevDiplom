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
    /// Логика взаимодействия для AddTypeDocWindow.xaml
    /// </summary>
    public partial class AddTypeDocWindow : Window
    {
        public AddTypeDocWindow()
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
                DBEntities.GetContext().DocumentsCategory.Add(new DocumentsCategory()
                {
                    NameCategory = PositionTb.Text
                });
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Формат успешно добавлен");
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
