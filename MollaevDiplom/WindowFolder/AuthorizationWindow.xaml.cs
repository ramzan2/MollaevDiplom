using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MollaevDiplom.ClassFolder;
using MollaevDiplom.DataFolder;
using MollaevDiplom.WindowFolder.AdminWindowFolder;
using MollaevDiplom.WindowFolder.DirectorWindowFolder;
using MollaevDiplom.WindowFolder.SecretaryWindowFolder;

namespace MollaevDiplom.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
      
        public AuthorizationWindow()
        {
            InitializeComponent();
            LoadSavedCredentials();
        }
        private void LoadSavedCredentials()
        {
            if (SettingsManager.IsRememberMeChecked)
            {
                LoginTb.Text = SettingsManager.SavedLogin;
                PasswordTb.Password = SettingsManager.SavedPassword;
                checkbox.IsChecked = true;
            }
        }

        public string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public void SaveRememberMeToken(int userId, string token)
        {
            using (var context = new DBEntities())
            {
                var rememberMeToken = new RemembersTokken
                {
                    IdUser = userId,
                    Tokken = token,
                    ExpiryDate = DateTime.Now.AddDays(30) // Токен действителен 30 дней
                };
                context.RemembersTokken.Add(rememberMeToken);
                context.SaveChanges();
            }
        }
        private void SetRememberMeToken(string token)
        {
            Properties.Settings.Default.RememberMeToken = token;
            Properties.Settings.Default.Save();
        }
        private void placeholderText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordTb.Focus();
        }


        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordTb.Focus();
                return;
            }
            else if (!string.IsNullOrWhiteSpace(LoginTb.Text) && !string.IsNullOrWhiteSpace(PasswordTb.Password))
            {
                try
                {
                    var user = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.LoginUser == LoginTb.Text);
                    if (checkbox.IsChecked == true)
                    {
                        var token = GenerateToken();
                        SaveRememberMeToken(user.IdUser, token);
                        SetRememberMeToken(token);

                        SettingsManager.SavedLogin = LoginTb.Text;
                        SettingsManager.SavedPassword = PasswordTb.Password;
                        SettingsManager.IsRememberMeChecked = true;
                    }
                    else
                    {
                        // Очистка сохраненных данных
                        SettingsManager.SavedLogin = string.Empty;
                        SettingsManager.SavedPassword = string.Empty;
                        SettingsManager.IsRememberMeChecked = false;
                    }

                    if (user == null)
                    {
                        MBClass.ErrorMB("Введен не правильный логин или пароль");
                        LoginTb.Focus();
                        Timer();
                        return;
                    }
                    else if(user.LoginUser != LoginTb.Text) 
                    {
                        MBClass.ErrorMB("Введен неверный логин");
                        LoginTb.Focus();
                        Timer();
                        return;
                    }
                    if (user.PasswordUser != PasswordTb.Password)
                    {
                        MBClass.ErrorMB("Введен не правильный логин или пароль");
                        PasswordTb.Focus();
                        Timer();

                        return;
                    }
                    else
                    {

                        switch (user.IdRole)
                        {
                            case 1:
                                new MenuDirecWindow().Show();
                                Close();
                                break;
                                     case 2:
                                new MenuSecretaryWindow().Show();
                                Close();
                                break;
                                        case 3:
                                new MenuAdminWindow().Show();
                                Close();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void Timer()
        {
            Fail = Fail + 1;
            if (Fail >= 3 && ActiveTime == false)
            {
                ActiveTime = true;
                CopyTime = (int)Time;
                double time = (int)Time + 0.5;
                LogInBtn.IsEnabled = false;
                LoginTb.IsReadOnly = true;
                Disable = 1;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(time) };
                timer.Start();
                TextTime();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    LogInBtn.Content = "Войти";
                    LogInBtn.IsEnabled = true;
                    LoginTb.IsReadOnly = false;
                    Time = Time * 1.5;
                    Fail = 2;
                    Disable = 0;
                    ActiveTime = false;
                };

            }
            return;
        }

        private int Disable = 0;
        private double Time = 10;
        private int Fail = 0;
        private bool ActiveTime = false;
        private double CopyTime = 10;
        private void TextTime()
        {
            if (CopyTime > 0)
            {
                int a = (int)CopyTime;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(1) };
                timer.Start();
                LogInBtn.Content = a;
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    CopyTime = CopyTime - 1;
                    TextTime();
                };
            }
        }

        private void LoginTb_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void PasswordTb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordTb.Password) && PasswordTb.Password.Length > 0)
                placeholderText.Visibility = Visibility.Collapsed;
            else
                placeholderText.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (LogInBtn.IsEnabled)
                {
                    LogInBtn_Click(sender, e);
                    return;
                }


                if (LoginTb.IsFocused)
                {
                    PasswordTb.Focus();
                }
                else
                {
                    LoginTb.Focus();
                }
            }
        }

        private void PasswordTb_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void PasswordTb_GotFocus(object sender, RoutedEventArgs e)
        {
          
        }

        private void HideIco_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LoginTBV.Visibility == Visibility.Visible)
            {
                LoginTBV.Visibility = Visibility.Hidden;
                PasswordTb.Visibility = Visibility.Visible;

                PasswordTb.Password = LoginTBV.Text;
            }
            else
            {
                LoginTBV.Visibility = Visibility.Visible;
                PasswordTb.Visibility = Visibility.Hidden;

                LoginTBV.Text = PasswordTb.Password;
            }
            HideIco.Visibility = Visibility.Collapsed;
            EyeIco.Visibility = Visibility.Visible;
        }

        private void HideIco_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideIco.Visibility = Visibility.Visible;
            EyeIco.Visibility = Visibility.Collapsed;
        }

        private void EyeIco_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideIco.Visibility = Visibility.Visible;
            EyeIco.Visibility = Visibility.Collapsed;

        }

        private void EyeIco_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (LoginTBV.Visibility == Visibility.Visible)
            {
                LoginTBV.Visibility = Visibility.Hidden;
                PasswordTb.Visibility = Visibility.Visible;

                PasswordTb.Password = LoginTBV.Text;
            }
            else
            {
                LoginTBV.Visibility = Visibility.Visible;
                PasswordTb.Visibility = Visibility.Hidden;

                LoginTBV.Text = PasswordTb.Password;
            }
            HideIco.Visibility = Visibility.Visible;
            EyeIco.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MBClass.InfoMB("Для восстановление пароля обратитесь к администратору!" +
                "Почта : admin@mail.ru");
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}

