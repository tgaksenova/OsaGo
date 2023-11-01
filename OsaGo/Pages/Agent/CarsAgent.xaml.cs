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

namespace OsaGo.Pages.Agent
{
    /// <summary>
    /// Логика взаимодействия для CarsAgent.xaml
    /// </summary>
    public partial class CarsAgent : Page
    {
        public CarsAgent()
        {
            InitializeComponent();
            DataGridCar.ItemsSource = Entitie.GetContext().Cars.ToList();
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var carsForRemoving = DataGridCar.SelectedItems.Cast<Cars>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {carsForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entitie.GetContext().Cars.RemoveRange(carsForRemoving);
                    Entitie.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridCar.ItemsSource = Entitie.GetContext().Cars.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CarsAdd(null));
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsAdd((sender as Button).DataContext as Cars));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entitie.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridCar.ItemsSource = Entitie.GetContext().Cars.ToList();
            }

        }
    }
}
