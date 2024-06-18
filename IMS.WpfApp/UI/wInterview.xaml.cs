using IMS.Business.Business;
using IMS.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace IMS.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wInterview.xaml
    /// </summary>
    public partial class wInterview : Window
    {
        private readonly InterviewsInfoBusiness _interviewBusiness;
        public wInterview()
        {
            _interviewBusiness ??= new InterviewsInfoBusiness();
            InitializeComponent();
            LoadGrdInterview();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtInterviewCode.Text == null) return;
                var interviewId = int.Parse(txtInterviewCode.Text);
                var item = await _interviewBusiness.GetByIdAsync(interviewId);

                if (item.Data == null)
                {
                    var interview = new InterviewsInfo()
                    {
                        Location = txtLocation.Text,
                        Result = txtResult.Text,
                        Name = txtName.Text,
                        Content = txtContent.Text
                    };

                    var result = await _interviewBusiness.AddAsync(interview);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var interview = item.Data as InterviewsInfo;
                    //interview.Time = txtTime.Text.;
                    interview.Location = txtLocation.Text;
                    interview.Result = txtResult.Text;
                    interview.Name = txtName.Text;
                    interview.Content = txtContent.Text;


                    var result = await _interviewBusiness.UpdateAsync(interview);
                    MessageBox.Show(result.Message, "Update");
                }

                txtLocation.Text = string.Empty;
                //txtLocation.Text = string.Empty;
                txtResult.Text = string.Empty;
                txtName.Text = string.Empty;
                txtContent.Text = string.Empty;
                this.LoadGrdInterview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void grdInterview_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as InterviewsInfo;
                    if (item != null)
                    {
                        var interviewResult = await _interviewBusiness.GetByIdAsync(item.InterviewinfoId);

                        if (interviewResult.Status > 0 && interviewResult.Data != null)
                        {
                            item = interviewResult.Data as InterviewsInfo;
                            txtLocation.Text = item.Location;
                            txtResult.Text = item.Result;
                            txtName.Text = item.Name;
                            txtContent.Text = item.Content;
                        }
                    }
                }
            }
        }

        private async void grdInterview_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //InterviewsInfo interview = await _interviewBusiness.GetByIdAsync(currencyCode);
                    //var result = await _interviewBusiness.DeleteAsync(currencyCode);
                    //MessageBox.Show($"{result.message}", "delete");
                    //this.LoadGrdInterview();
                }
            }
        }

        private async void LoadGrdInterview()
        {
            var result = await _interviewBusiness.GetAllAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdInterview.ItemsSource = result.Data as List<InterviewsInfo>;
            }
            else
            {
                grdInterview.ItemsSource = new List<InterviewsInfo>();
            }
        }
    }
}
