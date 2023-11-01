using OsaGo.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            Cmb.ItemsSource = Entitie.GetContext().Drivers.ToList();
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

            if (string.IsNullOrEmpty(TextBoxLogin.Text) ||
                string.IsNullOrEmpty(PasswordBox.Password) || string.IsNullOrEmpty(PasswordBoxCheck.Password))
            {
                MessageBox.Show("Введите все данные!");
            }

            
            //DataTable dt_user = Authorization.Select("SELECT * FROM [dbo].[Users] WHERE [Login] = '" + TextBoxLogin.Text);
            //if (dt_user.Rows.Count > 0) // если такая запись существует       
            //{
            //    MessageBox.Show("Пользователь с таким логином уже есть");
            //}


            if (PasswordBox.Password.Length >= 6)
            {

                bool en = true; // английская раскладка
                bool number = false; // цифра

                for (int i = 0; i < PasswordBox.Password.Length; i++) // перебираем символы
                {
                    if (PasswordBox.Password[i] >= 'А' && PasswordBox.Password[i] <= 'Я') en = false; // если русская раскладка
                    if (PasswordBox.Password[i] >= '0' && PasswordBox.Password[i] <= '9') number = true; //  цифры
                }

                if (!en)
                    MessageBox.Show("Доступна только английская раскладка");// выводим сообщение
                else if (!number)
                    MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
                if (en && number) // проверяем соответствие
                {

                }

                else { MessageBox.Show("Пароль слишком короткий, минимум 6 символов"); }
            }

            if (PasswordBox.Password == PasswordBoxCheck.Password) // проверка на совпадение паролей
            {
                MessageBox.Show("Пользователь зарегистрирован");
            }
            else MessageBox.Show("Пароли не совподают");

            
            Entitie db = new Entitie();
            Users userObject = new Users
            {
                Login = TextBoxLogin.Text,
                Password = PasswordBox.Password,
                Role = "Водитель"
            };
            
             db.Users.Add(userObject);
             db.SaveChanges();
        }

    }
}
