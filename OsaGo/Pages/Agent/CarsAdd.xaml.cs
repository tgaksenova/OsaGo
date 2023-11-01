using OsaGo.DataBase;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Car=OsaGo.DataBase.Cars;

namespace OsaGo.Pages.Agent
{
    /// <summary>
    /// Логика взаимодействия для CarsAdd.xaml
    /// </summary>
    public partial class CarsAdd : Page
    {
        private Car _currentCars = new Car();
        public CarsAdd(Car selectedCars)
        {
            InitializeComponent();
            if (selectedCars != null)
            {
                _currentCars = selectedCars;
            }
            DataContext = _currentCars;
            CmbCat.ItemsSource = Entitie.GetContext().Categories.ToList();
            CmbMark.ItemsSource = Entitie.GetContext().Marks.ToList();
            CmbMod.ItemsSource = Entitie.GetContext().Models.ToList();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Agent/CarsAgent.xaml", UriKind.Relative));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentCars.Category == null)
                errors.AppendLine("Выберите категорию!");
            if (_currentCars.Mark==null)
                errors.AppendLine("Укажите название марки!");
            if (_currentCars.Model==null)
                errors.AppendLine("Укажите название модели!");
            if (string.IsNullOrWhiteSpace(_currentCars.Year))
                errors.AppendLine("Укажите дату выпуска автомобиля!");
            if (string.IsNullOrWhiteSpace(_currentCars.Number))
                errors.AppendLine("Укажите гос.номер!");
            if (string.IsNullOrWhiteSpace(_currentCars.STSNumber))
                errors.AppendLine("Укажите номер СТС");
            if (string.IsNullOrWhiteSpace(_currentCars.PTSNumber))
                errors.AppendLine("Укажите номер ПТС");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }



            if (_currentCars.Id == 0)
                Entitie.GetContext().Cars.Add(_currentCars);
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
