using OsaGo.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

namespace OsaGo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public static Users UserUI;

        public Page1()
        {
            InitializeComponent();
        }

        private void PasswordBox_Changed(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility = Visibility.Visible;
            if (PasswordBox.Password.Length > 0)
            {
                txtHintPassword.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxLogin_Changed(object sender, RoutedEventArgs e)
        {
            txtHintLogin.Visibility = Visibility.Visible;
            if (TextBoxLogin.Text.Length > 0)
            {
                txtHintLogin.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            AuthTestSuccess(TextBoxLogin.Text, PasswordBox.Password);
        }

        public bool AuthTestSuccess(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(login, password);
                return false;
            }

            using (var db = new Entitie())
            {
                var user = db.Users.
                    AsNoTracking().
                    FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return false;
                }
                MessageBox.Show("Пользователь успешно найден!");
                TextBoxLogin.Clear();
                PasswordBox.Clear();
                UserUI = user;
                switch (user.Role.Trim())
                {
                    case "Страховой агент":
                        NavigationService?.Navigate(new Agenty());
                        break;

                    case "Водитель":
                        NavigationService?.Navigate(new Driver());
                        break;

                }
                
                return true;
            }
        }
        internal static DataTable Select(object value)
        {
            throw new NotImplementedException();
        }
    }
}
