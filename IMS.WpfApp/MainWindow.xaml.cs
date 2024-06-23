using IMS.WpfApp.UI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMS.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_wIntern_Click(object sender, RoutedEventArgs e)
        {
            var p = new wIntern();
            p.Owner = this;
            p.Show();
        }

        private void Open_wMentor_Click(object sender, RoutedEventArgs e)
        {
            var p = new wMentor();
            p.Owner = this;
            p.Show();
        }

        private void Open_wTask_Click(object sender, RoutedEventArgs e)
        {
            var p = new wTask();
            p.Owner = this;
            p.Show();
        }

        private void Open_wWorkResult_Click(object sender, RoutedEventArgs e)
        {
            var p = new wWorkResult();
            p.Owner = this;
            p.Show();
        }
    }
}