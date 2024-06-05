using IMS.Business.Business;
using IMS.Data.Models;
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
using System.Windows.Shapes;

namespace IMS.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wMentor.xaml
    /// </summary>
    public partial class wMentor : Window
    {
        private readonly MentorBusiness _mentorBusiness;
        public wMentor()
        {
            _mentorBusiness ??= new MentorBusiness();
            InitializeComponent();
            LoadGrdMentor();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _mentorBusiness.GetByIdAsync(txtCurrencyCode.Text as int?);

                if (item.Data == null)
                {
                    var mentor = new Mentor()
                    {
                        CurrencyCode = txtCurrencyCode.Text,
                        CurrencyName = txtCurrencyName.Text,
                        NationCode = txtNationCode.Text,
                        IsActive = chkIsActive.IsChecked
                    };

                    var result = await _business.Save(mentor);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var currency = item.Data as Currency;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    currency.CurrencyName = txtCurrencyName.Text;
                    currency.NationCode = txtNationCode.Text;
                    currency.IsActive = chkIsActive.IsChecked;

                    var result = await _business.Update(currency);
                    MessageBox.Show(result.Message, "Update");
                }

                txtCurrencyCode.Text = string.Empty;
                txtCurrencyName.Text = string.Empty;
                txtNationCode.Text = string.Empty;
                chkIsActive.IsChecked = false;
                this.LoadGrdCurrencies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void grdMentor_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
        }

        private void grdMentor_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void LoadGrdMentor()
        {
            var result = await _mentorBusiness.GetAllAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdMentor.ItemsSource = result.Data as List<Mentor>;
            }
            else
            {
                grdMentor.ItemsSource = new List<Mentor>();
            }
        }
    }
}
