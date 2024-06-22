using IMS.Business.Business;
using Models = IMS.Data.Models;
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
using IMS.Data.Models;

namespace IMS.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wTask.xaml
    /// </summary>
    public partial class wTask : Window
    {
        private readonly TaskBusiness _taskBusiness;
        public wTask()
        {
            _taskBusiness ??= new TaskBusiness();
            InitializeComponent();
            LoadGrdTask();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int taskId = -1;
                int.TryParse(txtTaskCode.Text, out taskId);
                var item = await _taskBusiness.FindAsync(taskId);

                if (item.Data == null)
                {
                    var task = new Models.Task()
                    {
                        Name = txtName.Text,
                        Description = txtDescription.Text,
                        Priority = int.Parse(txtPriority.Text),
                        Status = txtStatus.Text,
                        CreateDate = DateTime.Parse(txtCreateDate.Text),
                        DueDate = DateTime.Parse(txtDueDate.Text),
                        CompletionPercentage = int.Parse(txtCompletionPercentage.Text),
                        InternId = int.Parse(txtInternId.Text),
                        MentorId = int.Parse(txtMentorId.Text)



                    };

                    var result = await _taskBusiness.AddAsync(task);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var task = item.Data as Models.Task;
                    task.Name = txtName.Text;
                    task.Description = txtDescription.Text;
                    task.Priority = int.Parse(txtPriority.Text);
                    task.Status = txtStatus.Text;
                    task.CreateDate = DateTime.Parse(txtCreateDate.Text);
                    task.DueDate = DateTime.Parse(txtDueDate.Text);
                    task.CompletionPercentage = int.Parse(txtCompletionPercentage.Text);
                    task.InternId = int.Parse(txtInternId.Text);
                    task.MentorId = int.Parse(txtMentorId.Text);



                    var result = await _taskBusiness.UpdateAsync(task);
                    MessageBox.Show(result.Message, "Update");
                }

                txtTaskCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtPriority.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtCreateDate.Text = string.Empty;
                txtDueDate.Text = string.Empty;
                txtCompletionPercentage.Text = string.Empty;
                txtInternId.Text = string.Empty;
                txtMentorId.Text = string.Empty;
                LoadGrdTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtTaskCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPriority.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtCreateDate.Text = string.Empty;
            txtDueDate.Text = string.Empty;
            txtCompletionPercentage.Text = string.Empty;
            txtInternId.Text = string.Empty;
            txtMentorId.Text = string.Empty;


        }

        private async void grdTask_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Models.Task;
                    if (item != null)
                    {
                        var taskResult = await _taskBusiness.FindAsync(item.TaskId);

                        if (taskResult.Status > 0 && taskResult.Data != null)
                        {
                            item = taskResult.Data as Models.Task;
                            txtTaskCode.Text = item.TaskId.ToString();
                            txtName.Text = item.Name;
                            txtDescription.Text = item.Description;
                            txtPriority.Text = item.Priority.ToString();
                            txtStatus.Text = item.Status;
                            txtCreateDate.Text = item.CreateDate.ToString();
                            txtDueDate.Text = item.DueDate.ToString();
                            txtCompletionPercentage.Text = item.CompletionPercentage.ToString();
                            txtInternId.Text = item.InternId.ToString();
                            txtMentorId.Text = item.MentorId.ToString();
                        }
                    }
                }
            }
        }

        private async void grdTask_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // Attempt to convert CommandParameter to int
            int? taskId;
            if (btn.CommandParameter != null && int.TryParse(btn.CommandParameter.ToString(), out int value))
            {
                taskId = value;
            }
            else
            {
                // Handle the case where CommandParameter is not an integer or null
                MessageBox.Show("Invalid Task ID format. Please try again.", "Error", MessageBoxButton.OK);
                return;
            }

            // Confirmation and deletion logic (assuming InternId is valid)
            if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = await _taskBusiness.DeleteByIdAsync(taskId.Value);
                MessageBox.Show($"{result.Message}", "Delete");
                LoadGrdTask();
            }
        }

        private async void LoadGrdTask()
        {
            var result = await _taskBusiness.GetAllAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdTask.ItemsSource = null;
                grdTask.ItemsSource = result.Data as List<Models.Task>;
            }
            else
            {
                grdTask.ItemsSource = new List<Models.Task>();
            }
        }
    }
}
