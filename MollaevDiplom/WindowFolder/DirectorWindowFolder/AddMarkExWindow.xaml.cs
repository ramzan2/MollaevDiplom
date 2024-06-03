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
    /// Логика взаимодействия для AddMarkExWindow.xaml
    /// </summary>
    public partial class AddMarkExWindow : Window
    {
        public AddMarkExWindow()
        {
            InitializeComponent();
        }

        private void AddPsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PositionTb.Text))
            {
                MBClass.ErrorMB("Заполните поле");
            }
            else
            {
                DBEntities.GetContext().MarkExecution.Add(new MarkExecution()
                {
                    NameMarkExecution = PositionTb.Text
                });
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Отметка успешно добавлена");
                Close();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
