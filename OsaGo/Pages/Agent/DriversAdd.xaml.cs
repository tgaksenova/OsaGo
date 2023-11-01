using OsaGo.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using DriverBase = OsaGo.DataBase.Drivers;

namespace OsaGo.Pages.Agent
{
    /// <summary>
    /// Логика взаимодействия для DriversAdd.xaml
    /// </summary>
    public partial class DriversAdd : Page
    {
        private DriverBase _currentDriver = new DriverBase();
        public DriversAdd(DriverBase selectedDriver)
        {
            InitializeComponent();
            if (selectedDriver != null)
            {
                _currentDriver = selectedDriver;
            }
            DataContext = _currentDriver;
            CmbCity.ItemsSource = Entitie.GetContext().Regions.ToList();
        }
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Agent/Drivers.xaml", UriKind.Relative));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentDriver.Name))
                errors.AppendLine("Введите имя!");
            if (string.IsNullOrWhiteSpace(_currentDriver.Surname))
                errors.AppendLine("Введите фамилию!");
            if (string.IsNullOrWhiteSpace(_currentDriver.Patronymic))
                errors.AppendLine("Введите отчество!");
            if (_currentDriver.DateOfBirth == null)
                errors.AppendLine("Введите дату рождения!");
            if (_currentDriver.Region == null)
                errors.AppendLine("Укажите город!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentDriver.Id == 0)
                Entitie.GetContext().Drivers.Add(_currentDriver);
            try
            {
                Entitie.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
