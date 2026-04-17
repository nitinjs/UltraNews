using Nitin.News.BL;
using System;
using System.Collections.Generic;
using System.Device.Location;
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

namespace Nitin.News
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public bool IsCancelled { get; set; }
        LocationManager location;

        public SettingsWindow(LocationManager loc):base()
        {
            InitializeComponent();
            IsCancelled = false;
            location = loc;

            txtLocation.Text = Properties.Settings.Default.LocationName;

            var mode = Properties.Settings.Default.LocationMode;
            switch (mode)
            {
                case "manual":
                    rbManual.IsChecked = true;
                    txtLocation.IsEnabled = true;
                    break;
                case "gps":
                    rbInbuilt.IsChecked = true;
                    txtLocation.IsEnabled = false;
                    break;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsCancelled = true;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (rbInbuilt.IsChecked == true)
            {
                location.GetLocation();
                if (location.CurrentLocation.IsUnknown)
                {
                    this.Hide();
                    MessageBox.Show("Unable to get location from GPS, Device Returned: " + string.Format("{0}, {1}",
                location.CurrentLocation.Latitude,
                location.CurrentLocation.Longitude) + "\nPlease enter location manually.");
                    Properties.Settings.Default.LocationMode = "manual";
                    Properties.Settings.Default.LocationName = txtLocation.Text;
                    Properties.Settings.Default.Location = txtLocation.Text;

                    Properties.Settings.Default.Save();
                    IsCancelled = true;

                }
                else
                {
                    Properties.Settings.Default.LocationMode = "gps";
                    Properties.Settings.Default.Location = string.Format("{0}, {1}",
                    location.CurrentLocation.Latitude,
                    location.CurrentLocation.Longitude);

                    //get location name from web
                    Properties.Settings.Default.LocationName = location.LocationName;

                    MessageBox.Show("Detected location: " + Properties.Settings.Default.LocationName);

                    Properties.Settings.Default.Save();
                    IsCancelled = false;
                }
            }
            else
            {
                Properties.Settings.Default.LocationMode = "manual";
                Properties.Settings.Default.LocationName = txtLocation.Text;
                Properties.Settings.Default.Location = txtLocation.Text;

                Properties.Settings.Default.Save();
                IsCancelled = false;
            }
            this.Close();
        }

        //<!--New York, USA-->

        private void rbInbuilt_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtLocation.IsEnabled = (rbInbuilt.IsChecked==false);
                txtLocation.Text = Properties.Settings.Default.LocationName;
            }
            catch (Exception)
            {
                
            }
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)
        {
            rbManual.IsChecked = true;
            txtLocation.IsEnabled = true;
            txtLocation.Text = "New York, USA";
            IsCancelled = false;
        }
    }
}
