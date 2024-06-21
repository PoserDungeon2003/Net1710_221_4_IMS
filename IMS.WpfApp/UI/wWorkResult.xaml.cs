using IMS.Business.Business;
using IMS.Data.Models;
using IMS.Data.Repository;
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
using System.ComponentModel;

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
            var WorkResult = new WorkingResult()
            {
                CreatedBy = txtCreatedBy.Text,
                Rating = double.Parse(txtRating.Text),
                Note = txtNote.Text,
                DateCompleted = DateTime.Parse(txtDateCompleted.Text),
                Status = txtStatus.Text,
                HoursWorked = int.Parse(txtHoursWork.Text),
                MentorId = int.Parse(txtMentorID.Text),
                TaskId = int.Parse(txtTaskID.Text),
                InternId = int.Parse(txtInternID.Text),
            };
            _workingResultBusiness.AddAsync(WorkResult);
            LoadGrdWorkResult();
        }

        private void txtResultID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}