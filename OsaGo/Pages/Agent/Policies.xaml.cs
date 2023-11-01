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
using Poly = OsaGo.DataBase.Policies;

namespace OsaGo.Pages.Agent
{
    /// <summary>
    /// Логика взаимодействия для Policies.xaml
    /// </summary>
    public partial class Policies : Page
    {
        public Policies()
        {
            InitializeComponent(); 
            DataGridPoly.ItemsSource = Entitie.GetContext().Policies.ToList();

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PoliciesAdd((sender as Button).DataContext as Poly));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PoliciesAdd(null));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var polyForRemoving = DataGridPoly.SelectedItems.Cast<Poly>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {polyForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entitie.GetContext().Policies.RemoveRange(polyForRemoving);
                    Entitie.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridPoly.ItemsSource = Entitie.GetContext().Policies.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }
    }
}
