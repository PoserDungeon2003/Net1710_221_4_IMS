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
                if (txtTaskCode.Text == null) return;
                var taskId = int.Parse(txtTaskCode.Text);
                var item = await _taskBusiness.GetByIdAsync(taskId);

                if (item.Data == null)
                {
                    var task = new Models.Task()
                    {
                        Description = txtDescription.Text,
                        InternId = txtInternID.Text,
                        Name = txtName.Text,
                        MentorId = txtMentorID.Text,
                        
                    };

                    var result = await _taskBusiness.AddAsync(task);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var task = item.Data as Models.Task;
                    task.Description = txtName.Text;
                    task.InternId = txtEmail.Text;
                    task.Name = txtPhone.Text;
                    task.MentorId = txtJobPosition.Text;

                    var result = await _taskBusiness.UpdateAsync(task);
                    MessageBox.Show(result.Message, "Update");
                }

                txtMentorCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtCompany.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtJobPosition.Text = string.Empty;
                this.LoadGrdTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
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
                        var taskResult = await _taskBusiness.GetByIdAsync(item.TaskId);

                        if (taskResult.Status > 0 && taskResult.Data != null)
                        {
                            item = taskResult.Data as Models.Task;
                            txtMentorCode.Text = item.MentorId.ToString();
                            txtName.Text = item.FullName;
                            txtCompany.Text = item.CompanyId.ToString();
                            txtEmail.Text = item.Email;
                            txtPhone.Text = item.Phone;
                            txtJobPosition.Text = item.JobPosition;
                        }
                    }
                }
            }
        }

        private async void grdTask_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //var result = await _taskBusiness.DeleteAsync(currencyCode);
                    //MessageBox.Show($"{result.Message}", "Delete");
                    //this.LoadGrdCurrencies();
                }
            }
        }

        private async void LoadGrdTask()
        {
            var result = await _taskBusiness.GetAllAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdTask.ItemsSource = result.Data as List<Models.Task>;
            }
            else
            {
                grdTask.ItemsSource = new List<Models.Task>();
            }
        }
    }
}
