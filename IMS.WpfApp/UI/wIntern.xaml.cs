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
                int internId = -1;
                int.TryParse(txtInternId.Text, out internId);
                var item = await _business.GetByID(internId);

                if (item.Data == null)
                {
                    var intern = new Intern()
                    {
                        // InternId = int.Parse(txtInternId.Text),
                        University = txtUniversity.Text,
                        Major = txtMajor.Text,
                        JobPosition = txtJobPosition.Text,
                        EducationBackground = txtEducationBackground.Text,
                        Experiences = txtExperiences.Text,
                        WorkingTasks = txtWorkingTasks.Text,
                        MentorId = int.Parse(txtMentorId.Text),
                        Name = txtName.Text,
                        CompanyId = int.Parse(txtCompanyId.Text),
                    };

                    var result = await _business.Save(intern);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var intern = item.Data as Intern;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    intern.University = txtUniversity.Text;
                    intern.Major = txtMajor.Text;
                    intern.JobPosition = txtJobPosition.Text;
                    intern.EducationBackground = txtEducationBackground.Text;
                    intern.Experiences = txtExperiences.Text;
                    intern.WorkingTasks = txtWorkingTasks.Text;
                    intern.MentorId = int.Parse(txtMentorId.Text);
                    intern.Name = txtName.Text;
                    intern.CompanyId = int.Parse(txtCompanyId.Text);

                    var result = await _business.Update(intern);
                    MessageBox.Show(result.Message, "Update");
                }

                txtInternId.Text = string.Empty;
                txtUniversity.Text = string.Empty;
                txtMajor.Text = string.Empty;
                txtJobPosition.Text = string.Empty;
                txtEducationBackground.Text = string.Empty;
                txtExperiences.Text = string.Empty;
                txtWorkingTasks.Text = string.Empty;
                txtMentorId.Text = string.Empty;
                txtName.Text = string.Empty;
                txtCompanyId.Text = string.Empty;
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
        private async void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Intern;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetByID(item.InternId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Intern;
                            txtInternId.Text = item.InternId.ToString();
                            txtUniversity.Text = item.University;
                            txtMajor.Text = item.Major;
                            txtJobPosition.Text = item.JobPosition;
                            txtEducationBackground.Text = item.EducationBackground;
                            txtExperiences.Text = item.Experiences;
                            txtWorkingTasks.Text = item.WorkingTasks;
                            txtMentorId.Text = item.MentorId.ToString();
                            txtName.Text = item.Name;
                            txtCompanyId.Text = item.CompanyId.ToString();
                        }
                    }
                }
            }
        }
        private async void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // Attempt to convert CommandParameter to int
            int? InternId;
            if (btn.CommandParameter != null && int.TryParse(btn.CommandParameter.ToString(), out int value))
            {
                InternId = value;
            }
            else
            {
                // Handle the case where CommandParameter is not an integer or null
                MessageBox.Show("Invalid Intern ID format. Please try again.", "Error", MessageBoxButton.OK);
                return;
            }

            // Confirmation and deletion logic (assuming InternId is valid)
            if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = await _business.DeleteById(InternId.Value);
                MessageBox.Show($"{result.Message}", "Delete");
                this.LoadGrdIntern();
            }
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

        private void txtUniversity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
