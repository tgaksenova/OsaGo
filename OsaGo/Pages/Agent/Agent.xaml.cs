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

namespace OsaGo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Agent.xaml
    /// </summary>
    public partial class Agenty : Page
    {
        public Agenty()
        {
            InitializeComponent();
        }
        private void ButtonClickD(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Agent/Drivers.xaml", UriKind.Relative));
        }
        private void ButtonClickC(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Agent/CarsAgent.xaml", UriKind.Relative));
        }
        private void ButtonClickP(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Agent/Policies.xaml", UriKind.Relative));
        }
    }
}
