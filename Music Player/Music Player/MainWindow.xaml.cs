using MahApps.Metro.IconPacks;
using Music_Player.ViewModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Threading;


namespace Music_Player
{

    public partial class MainWindow : Window
    {
        private bool isPlaying = true;
        private SongViewModel _songViewModel;

        private DispatcherTimer timer;
        private bool isDragging = false;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = @"C:\Users",
                IsFolderPicker = true 
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
                _songViewModel = new SongViewModel(dialog.FileName);
                this.DataContext = _songViewModel;

                _songViewModel.PlaySongAction = (filePath) =>
                {
                    mediaElement.Source = new Uri(filePath, UriKind.RelativeOrAbsolute);
                    mediaElement.Play();
                    playPauseIcon.Kind = PackIconMaterialKind.Pause;
                    mediaElement.MediaEnded += MediaElement_MediaEnded;

                };
            }
            else
            {
                _songViewModel = new SongViewModel(@"C:\Users\Cifer\Music");
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_songViewModel != null && _songViewModel.NextSongCommand.CanExecute(null))
            {
                _songViewModel.NextSongCommand.Execute(null);
            }
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Do you really want to EXIT ???", "EXIT", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (answer == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_PausePlay(object sender, RoutedEventArgs e)
        {
            if (_songViewModel.ActiveSong == null)
            {
                var firstSong = _songViewModel.Songs.First();
                _songViewModel.SetActiveSong(firstSong);
                isPlaying = false;
                playPauseIcon.Kind = PackIconMaterialKind.Play;
            }

            if (isPlaying)
            {
                mediaElement.Pause();
                isPlaying = false;
                playPauseIcon.Kind = PackIconMaterialKind.Play;
            }
            else
            {
                mediaElement.Play();
                isPlaying = true;
                playPauseIcon.Kind = PackIconMaterialKind.Pause;
            }
        }
        private void slider_VolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
            {
                double volumeFactor = 10;
                mediaElement.Volume = slider.Value/volumeFactor;
            }
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_songViewModel != null && _songViewModel.NextSongCommand.CanExecute(null))
            {
                _songViewModel.NextSongCommand.Execute(null);
                isPlaying = true;
                playPauseIcon.Kind = PackIconMaterialKind.Pause;
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (_songViewModel != null && _songViewModel.PreviousSongCommand.CanExecute(null))
            {
                _songViewModel.PreviousSongCommand.Execute(null);
                isPlaying = true;
                playPauseIcon.Kind = PackIconMaterialKind.Pause;
            }
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
                _songViewModel = new SongViewModel(dialog.FileName);
                this.DataContext = _songViewModel;

                _songViewModel.PlaySongAction = (filePath) =>
                {
                    mediaElement.Source = new Uri(filePath, UriKind.RelativeOrAbsolute);
                    mediaElement.Play();
                    isPlaying = true;
                    playPauseIcon.Kind = PackIconMaterialKind.Pause;
                    mediaElement.MediaEnded += MediaElement_MediaEnded;
                };
            }
        }

        private void SeekBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(e.NewValue - mediaElement.Position.TotalSeconds) > 1)
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }


        private void OpenMusicPlayer_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            _songViewModel.ShowVideoView();
        }

        private void sliderProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isDragging && mediaElement.NaturalDuration.HasTimeSpan)
            {
                mediaElement.Position = TimeSpan.FromSeconds(sliderProgress.Value);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging && mediaElement.NaturalDuration.HasTimeSpan)
            {
                sliderProgress.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                sliderProgress.Value = mediaElement.Position.TotalSeconds;
            }
        }
        private void sliderProgress_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true; // Ngăn không cho Timer cập nhật thanh trượt khi đang kéo
        }
        private void sliderProgress_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            mediaElement.Position = TimeSpan.FromSeconds(sliderProgress.Value); // Cập nhật vị trí phát nhạc
        }

        private void sliderProgress_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(sliderProgress);


            double newValue = position.X / sliderProgress.ActualWidth * sliderProgress.Maximum;


            sliderProgress.Value = newValue;


            sliderProgress_ValueChanged(sender, new RoutedPropertyChangedEventArgs<double>(sliderProgress.Value, newValue));
        }
    }
}