using Microsoft.Win32;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        private DispatcherTimer timer;
        private string selectedFilePath;

        public VideoPlayer()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                sliderProgress.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                sliderProgress.Value = mediaElement.Position.TotalSeconds;
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFilePath != null)
            {
                mediaElement.Play();
                timer.Start();
            }
            else
            {
                MessageBox.Show("Please select an MP4 file first.");
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            timer.Stop();
            sliderProgress.Value = 0;
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "MP4 Files (*.mp4)|*.mp4|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                mediaElement.Source = new Uri(selectedFilePath);
                MessageBox.Show($"Selected File is: {selectedFilePath}");
            }
        }

        private void sliderProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                mediaElement.Position = TimeSpan.FromSeconds(sliderProgress.Value);
            }
        }
    }
}
