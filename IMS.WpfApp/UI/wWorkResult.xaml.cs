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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMS.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wWorkResult.xaml
    /// </summary>
    public partial class wWorkResult : Window
    {
        private readonly WorkingResultBusiness _workingResultBusiness;
        public wWorkResult()
        {
            _workingResultBusiness ??= new WorkingResultBusiness();
            InitializeComponent();
            LoadGrdWorkResult();
            
        }
        public async void LoadGrdWorkResult()
        {
            var result = await _workingResultBusiness.GetAllAsync();
            if (result.Status > 0 && result.Data != null)
            {
                grdWorkResult.ItemsSource = (IEnumerable<WorkingResult>)result.Data;
            }
            else
            {
                MessageBox.Show(result.Message, "Load");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}