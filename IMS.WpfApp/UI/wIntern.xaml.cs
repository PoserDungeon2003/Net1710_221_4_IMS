using IMS.Business.Business;
using IMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
