using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;

namespace WPF_Overconsumption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ConsumptionCalc> lstCalcs { get; set; } = new ObservableCollection<ConsumptionCalc>();

        public MainWindow()
        {
            InitializeComponent();

            lstCalcs.Add(new ConsumptionCalc());

            dgvCalc.SelectedIndex = 0;
            
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();
            if (_openFileDialog.ShowDialog() == true)
            {
                try
                {
                    XmlSerializer _xmlFormatter = new XmlSerializer(typeof(ObservableCollection<ConsumptionCalc>));
                    using (Stream _fileStream = new FileStream(_openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        _fileStream.Position = 0;
                        lstCalcs = (ObservableCollection<ConsumptionCalc>)_xmlFormatter.Deserialize(_fileStream);
                    }

                    this.Title = "Overconsumption Calculator v1.0 - " + _openFileDialog.FileName;

                    dgvCalc.ItemsSource = lstCalcs;

                }
                catch (Exception)
                {
                    MessageBox.Show("Please try open the correct file type.");
                }
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog();
            if (_saveFileDialog.ShowDialog() == true)
            {
                XmlSerializer _xmlFormatter = new XmlSerializer(typeof(ObservableCollection<ConsumptionCalc>));
                using (Stream _fileStream = new FileStream(_saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    _fileStream.Position = 0;
                    _xmlFormatter.Serialize(_fileStream, lstCalcs);
                }
            }
        }

        private void DgvCalc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbFlowMeter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedIndex == 1) // Weight
            {
                grdData.RowDefinitions[6].Height = new GridLength(0);
                grdData.RowDefinitions[7].Height = new GridLength(0);
                grdData.RowDefinitions[8].Height = new GridLength(0);
                grdData.RowDefinitions[9].Height = new GridLength(0);
                grdData.RowDefinitions[10].Height = new GridLength(30);
                grdData.RowDefinitions[11].Height = new GridLength(0);
            }

            if (cb.SelectedIndex == 0)
            {
                grdData.RowDefinitions[6].Height = new GridLength(30);
                grdData.RowDefinitions[7].Height = new GridLength(30);
                grdData.RowDefinitions[8].Height = new GridLength(30);
                grdData.RowDefinitions[9].Height = new GridLength(30);
                grdData.RowDefinitions[10].Height = new GridLength(0);
                grdData.RowDefinitions[11].Height = new GridLength(30);
            }
        }
    }
}
