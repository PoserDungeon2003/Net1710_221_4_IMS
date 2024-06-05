using IMS.Business.Business;
using IMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Interaction logic for wIntern.xaml
    /// </summary>
    public partial class wIntern : Window
    {
        private readonly InternBusiness _business;
        public wIntern()
        {
            InitializeComponent();
            this._business ??= new InternBusiness();
            this.LoadGrdIntern();
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtCurrencyCode.Text, out idTmp);

                var item = await _business.GetByID(int.Parse(txtInternId.Text));

                if (item.Data == null)
                {
                    var intern = new Intern()
                    {
                        InternId = int.Parse(txtInternId.Text),
                        University = txtNationCode.Text,
                        Major = txtCurrencyName.Text,
                        JobPosition = txtCurrencyName.Text,
                        EducationBackground = txtCurrencyName.Text,
                        Experiences = txtCurrencyName.Text,
                        WorkingTasks = txtCurrencyName.Text,
                        MentorId = int.Parse(txtInternId.Text),
                        Name = txtCurrencyName.Text,
                        CompanyId = int.Parse(txtInternId.Text),
                    };

                    var result = await _business.Save(intern);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var intern = item.Data as Intern;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    //intern.InternId = txtCurrencyName.Text;
                    //intern.NationCode = txtNationCode.Text;
                    //intern.IsActive = chkIsActive.IsChecked;

                    var result = await _business.Update(intern);
                    MessageBox.Show(result.Message, "Update");
                }

                txtInternId.Text = string.Empty;
                txtCurrencyName.Text = string.Empty;
                txtNationCode.Text = string.Empty;
                chkIsActive.IsChecked = false;
                this.LoadGrdIntern();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }
        private void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
        }
        private void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void LoadGrdIntern()
        {
            var result = await _business.Getall();

            if (result.Status > 0 && result.Data != null)
            {
                grdCurrency.ItemsSource = result.Data as List<Intern>;
            }
            else
            {
                grdCurrency.ItemsSource = new List<Intern>();
            }
        }

        private void grdCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtInterId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
