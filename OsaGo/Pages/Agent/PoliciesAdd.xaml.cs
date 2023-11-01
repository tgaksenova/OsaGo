using OsaGo.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using Poly = OsaGo.DataBase.Policies;

namespace OsaGo.Pages.Agent
{
    /// <summary>
    /// Логика взаимодействия для PoliciesAdd.xaml
    /// </summary>
    public partial class PoliciesAdd : Page
    {
        private Poly _currentPoly = new Poly();
        public PoliciesAdd(Poly selectedPoly)
        {
            InitializeComponent();
            if (selectedPoly != null)
            {
                _currentPoly = selectedPoly;
            }
            DataContext = _currentPoly;
            CmbCar.ItemsSource = Entitie.GetContext().Cars.ToList();
            CmbDr.ItemsSource = Entitie.GetContext().Drivers.ToList();
            CmbCompany.ItemsSource = Entitie.GetContext().InsuranceCopmanyes.ToList();
        }

        private void TextBoxPrice_Changed(object sender, TextChangedEventArgs e)
        {
            txtHintPrice.Visibility = Visibility.Visible;
            if (TextBoxPrice.Text.Length > 0)
            {
                txtHintPrice.Visibility = Visibility.Hidden;
            }
        }
        private void Formalization_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (CmbDr.SelectedValue == null)
                errors.AppendLine("Выберите Id водителя!");
            if (CmbDr.SelectedValue == null)
                errors.AppendLine("Выберите Id машины!");
            if (CmbCompany.SelectedValue == null)
                errors.AppendLine("Выберите страховую компанию!");
            if (ReggiDate.SelectedDate.Value == default)
                errors.AppendLine("Укажите дату регистрации!");
            if (VallyDate.SelectedDate.GetValueOrDefault() == default)
                errors.AppendLine("Укажите срок действия!");
            if (TextBoxPrice.Text == "0" || string.IsNullOrEmpty(TextBoxPrice.Text))
                errors.AppendLine("Введите стоимость!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (Convert.ToInt64(_currentPoly.Number) != 0)
                Entitie.GetContext().Policies.Add(_currentPoly);
            try
            {
                _currentPoly.Number = (long.Parse(Entitie.GetContext().Policies.OrderByDescending(x => x.Number).First().Number) + 1).ToString();
                var driver = Entitie.GetContext().Drivers.Where(x => x.Id == _currentPoly.DriverId).FirstOrDefault();
                var license = Entitie.GetContext().Licenses.Where(x => x.DriverId == driver.Id).FirstOrDefault();
                _currentPoly.LicenseNumber = license.Number;
                _currentPoly.LicenseSeries = license.Series;
                Entitie.GetContext().Policies.Add(_currentPoly);
                Entitie.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                MessageBox.Show(exceptionMessage.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
