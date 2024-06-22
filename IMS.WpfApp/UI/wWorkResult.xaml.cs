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
using System.Xml.Serialization;

namespace IMS.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wWorkResult.xaml
    /// </summary>
    public partial class wWorkResult : Window
    {
        private readonly WorkingResultBusiness _workingResultBusiness;
        public WorkingResult workingResult { get; set; }

        private bool _suppressSelectionChanged = false;


        public wWorkResult()
        {
            _workingResultBusiness ??= new WorkingResultBusiness();
            InitializeComponent();
            LoadGrdWorkResult();
            workingResult = new WorkingResult();
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
        public void LoadGrdWorkResultBox(WorkingResult workingResult)
        {
            txtCreatedBy.Text = workingResult.CreatedBy;
            txtRating.Text = workingResult.Rating.ToString();
            txtNote.Text = workingResult.Note;
            txtDateCompleted.Text = workingResult.DateCompleted.ToString();
            txtStatus.Text = workingResult.Status;
            txtHoursWork.Text = workingResult.HoursWorked.ToString();
            txtMentorID.Text = workingResult.MentorId.ToString();
            txtTaskID.Text = workingResult.TaskId.ToString();
            txtInternID.Text = workingResult.InternId.ToString();
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtCreatedBy.Text = "";
                txtRating.Text = "";
                txtNote.Text = "";
                txtDateCompleted.Text = "";
                txtStatus.Text = "";
                txtHoursWork.Text = "";
                txtMentorID.Text = "";
                txtTaskID.Text = "";
                txtInternID.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Cancel");
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Save");
            }
            
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (grdWorkResult.SelectedItem != null)
                {
                    var selectedResult = (WorkingResult)grdWorkResult.SelectedItem;
                    selectedResult.CreatedBy = txtCreatedBy.Text;
                    selectedResult.Rating = double.Parse(txtRating.Text);
                    selectedResult.Note = txtNote.Text;
                    selectedResult.DateCompleted = DateTime.Parse(txtDateCompleted.Text);
                    selectedResult.Status = txtStatus.Text;
                    selectedResult.HoursWorked = int.Parse(txtHoursWork.Text);
                    selectedResult.MentorId = int.Parse(txtMentorID.Text);
                    selectedResult.TaskId = int.Parse(txtTaskID.Text);
                    selectedResult.InternId = int.Parse(txtInternID.Text);

                    _workingResultBusiness.Update(selectedResult);
                    LoadGrdWorkResult();
                }
                else
                {
                    MessageBox.Show("Please select a working result to update.", "Update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update");
            }
        }

        private void grdWorkResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_suppressSelectionChanged)
            {
                return;
            }
            try
            {
                workingResult.ResultId = (grdWorkResult.SelectedItem as WorkingResult).ResultId;
                workingResult.CreatedBy = (grdWorkResult.SelectedItem as WorkingResult).CreatedBy;
                workingResult.Rating = (grdWorkResult.SelectedItem as WorkingResult).Rating;
                workingResult.Note = (grdWorkResult.SelectedItem as WorkingResult).Note;
                workingResult.DateCompleted = (grdWorkResult.SelectedItem as WorkingResult).DateCompleted;
                workingResult.Status = (grdWorkResult.SelectedItem as WorkingResult).Status;
                workingResult.HoursWorked = (grdWorkResult.SelectedItem as WorkingResult).HoursWorked;
                workingResult.MentorId = (grdWorkResult.SelectedItem as WorkingResult).MentorId;
                workingResult.TaskId = (grdWorkResult.SelectedItem as WorkingResult).TaskId;
                workingResult.InternId = (grdWorkResult.SelectedItem as WorkingResult).InternId;
                LoadGrdWorkResultBox(workingResult);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _suppressSelectionChanged = true;
                var result = _workingResultBusiness.Delete(workingResult);
                LoadGrdWorkResult();
                grdWorkResult.UnselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                _suppressSelectionChanged = false;
            }

        }
    }
}