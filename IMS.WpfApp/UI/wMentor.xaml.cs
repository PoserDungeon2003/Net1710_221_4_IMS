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
                int mentorId = -1;
                int.TryParse(txtMentorCode.Text, out mentorId);
                var item = await _mentorBusiness.FindAsync(mentorId);

                if (item.Data == null)
                {
                    var mentor = new Mentor()
                    {
                        FullName = txtName.Text,
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        JobPosition = txtJobPosition.Text,
                        CompanyId = int.Parse(txtCompany.Text)
                    };

                    var result = await _mentorBusiness.AddAsync(mentor);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var mentor = item.Data as Mentor;
                    mentor.FullName = txtName.Text;
                    mentor.Email = txtEmail.Text;
                    mentor.Phone = txtPhone.Text;
                    mentor.JobPosition = txtJobPosition.Text;

                    var result = await _mentorBusiness.UpdateAsync(mentor);
                    MessageBox.Show(result.Message, "Update");
                }

                txtMentorCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtCompany.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtJobPosition.Text = string.Empty;
                this.LoadGrdMentor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void grdMentor_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Mentor;
                    if (item != null)
                    {
                        var mentorResult = await _mentorBusiness.FindAsync(item.MentorId);

                        if (mentorResult.Status > 0 && mentorResult.Data != null)
                        {
                            item = mentorResult.Data as Mentor;
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

        private async void grdMentor_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //var result = await _mentorBusiness.DeleteAsync(currencyCode);
                    //MessageBox.Show($"{result.Message}", "Delete");
                    //this.LoadGrdCurrencies();
                }
            }
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
