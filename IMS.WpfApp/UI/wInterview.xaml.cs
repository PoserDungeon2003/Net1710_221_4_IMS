using IMS.Business.Business;
using IMS.Data.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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
                int interviewId = -1;
                int.TryParse(txtInterviewCode.Text, out interviewId);
                var item = await _interviewBusiness.FindAsync(interviewId);

                if (item.Data == null)
                {
                    var interview = new InterviewsInfo()
                    {
                        Time = DateTime.Parse(txtTime.Text),
                        Location = txtLocation.Text,
                        Result = txtResult.Text,
                        Position = txtPosition.Text,
                        InterviewStatus = txtStatus.Text,
                        Content = txtContent.Text,
                        MentorId = int.Parse(txtMentor.Text),
                        InternId = int.Parse(txtIntern.Text),
                        InterviewMode = txtMode.Text,
                        Feedback = txtFeedback.Text,
                    };

                    var result = await _interviewBusiness.AddAsync(interview);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var interview = item.Data as InterviewsInfo;
                    interview.Time = DateTime.Parse(txtTime.Text);
                    interview.Location = txtLocation.Text;
                    interview.Result = txtResult.Text;
                    interview.Position = txtPosition.Text;
                    interview.InterviewStatus = txtStatus.Text;
                    interview.Content = txtContent.Text;
                    interview.MentorId = int.Parse(txtMentor.Text);
                    interview.InternId = int.Parse(txtIntern.Text);
                    interview.InterviewMode = txtMode.Text;
                    interview.Feedback = txtFeedback.Text;


                    var result = await _interviewBusiness.UpdateAsync(interview);
                    MessageBox.Show(result.Message, "Update");
                }
                txtInterviewCode.Text = string.Empty;
                txtTime.Text = string.Empty;
                txtLocation.Text = string.Empty;
                txtResult.Text = string.Empty;
                txtPosition.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtContent.Text = string.Empty;
                txtMentor.Text = string.Empty;
                txtIntern.Text = string.Empty;
                txtMode.Text = string.Empty;
                txtFeedback.Text = string.Empty;
                this.LoadGrdInterview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtInterviewCode.Text = string.Empty;
            txtTime.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtContent.Text = string.Empty;
            txtMentor.Text = string.Empty;
            txtIntern.Text = string.Empty;
            txtMode.Text = string.Empty;
            txtFeedback.Text = string.Empty;
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
                        var interviewResult = await _interviewBusiness.FindAsync(item.InterviewinfoId);

                        if (interviewResult.Status > 0 && interviewResult.Data != null)
                        {
                            item = interviewResult.Data as InterviewsInfo;
                            txtInterviewCode.Text = item.InterviewinfoId.ToString();
                            txtTime.Text = item.Time.ToString();
                            txtLocation.Text = item.Location;
                            txtResult.Text = item.Result;
                            txtPosition.Text = item.Position;
                            txtStatus.Text = item.InterviewStatus;
                            txtContent.Text = item.Content;
                            txtMentor.Text = item.MentorId.ToString();
                            txtIntern.Text = item.InternId.ToString();

                            //txtMentorId.Text = item.MentorId.ToString();
                            //txtInternId.Text = item.InternId.ToString();
                            txtMode.Text = item.InterviewMode;
                            txtFeedback.Text = item.Feedback;

                        }
                    }
                }
            }
        }

        private async void grdInterview_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // Attempt to convert CommandParameter to int
            int? interviewId;
            if (btn.CommandParameter != null && int.TryParse(btn.CommandParameter.ToString(), out int value))
            {
                interviewId = value;
            }
            else
            {
                // Handle the case where CommandParameter is not an integer or null
                MessageBox.Show("Invalid Interview ID format. Please try again.", "Error", MessageBoxButton.OK);
                return;
            }

            // Confirmation and deletion logic (assuming InternId is valid)
            if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = await _interviewBusiness.DeleteByIdAsync(interviewId.Value);
                MessageBox.Show($"{result.Message}", "Delete");
                LoadGrdInterview();
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