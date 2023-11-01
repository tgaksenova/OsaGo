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
using DriverBase = OsaGo.DataBase.Drivers;

namespace OsaGo.Pages.Agent
{
    /// <summary>
    /// Логика взаимодействия для Drivers.xaml
    /// </summary>
    public partial class Drivers : Page
    {
        public Drivers()
        {
            InitializeComponent();
            DataGridDriver.ItemsSource = Entitie.GetContext().Drivers.ToList();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DriversAdd(null));
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DriversAdd((sender as Button).DataContext as DriverBase));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var driversForRemoving = DataGridDriver.SelectedItems.Cast<DriverBase>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {driversForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entitie.GetContext().Drivers.RemoveRange(driversForRemoving);
                    Entitie.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridDriver.ItemsSource = Entitie.GetContext().Drivers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }
    }
}
