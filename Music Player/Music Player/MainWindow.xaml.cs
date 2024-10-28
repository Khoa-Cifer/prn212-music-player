using MahApps.Metro.IconPacks;
using Music_Player.ViewModel;
<<<<<<< Updated upstream
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace Music_Player
{

=======
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Threading;

namespace Music_Player
{
>>>>>>> Stashed changes
    public partial class MainWindow : Window
    {
        private bool isPlaying = true;
        private SongViewModel _songViewModel;
<<<<<<< Updated upstream
=======
        private DispatcherTimer _timer;
>>>>>>> Stashed changes

        public MainWindow()
        {
            InitializeComponent();

            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = @"C:\Users",
<<<<<<< Updated upstream
                IsFolderPicker = true 
=======
                IsFolderPicker = true
>>>>>>> Stashed changes
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                _songViewModel = new SongViewModel(dialog.FileName);
            }
            else
            {
                _songViewModel = new SongViewModel(@"C:\Users\Cifer\Music");
            }
<<<<<<< Updated upstream
=======

            InitializeMediaTimer();
        }

        private void InitializeMediaTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateSeekBar;
        }

        private void UpdateSeekBar(object sender, EventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
                TotalDurationText.Text = duration.ToString(@"mm\:ss");

                TimeSpan currentTime = mediaElement.Position;
                CurrentTimeText.Text = currentTime.ToString(@"mm\:ss");

                SeekBar.Maximum = duration.TotalSeconds;
                SeekBar.Value = currentTime.TotalSeconds;
            }
>>>>>>> Stashed changes
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
=======
            _timer.Stop(); // Stop the timer when the song ends

>>>>>>> Stashed changes
            if (_songViewModel != null && _songViewModel.NextSongCommand.CanExecute(null))
            {
                _songViewModel.NextSongCommand.Execute(null);
            }
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            MessageBoxResult answer = MessageBox.Show("Do you really want to quit??", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
=======
            MessageBoxResult answer = MessageBox.Show("Do you really want to quit?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
>>>>>>> Stashed changes
            if (answer == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_PausePlay(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                mediaElement.Pause();
                isPlaying = false;
                playPauseIcon.Kind = PackIconMaterialKind.Play;
<<<<<<< Updated upstream
=======
                _timer.Stop(); // Stop updating the SeekBar
>>>>>>> Stashed changes
            }
            else
            {
                mediaElement.Play();
                isPlaying = true;
                playPauseIcon.Kind = PackIconMaterialKind.Pause;
<<<<<<< Updated upstream
            }
        }
=======
                _timer.Start(); // Start updating the SeekBar
            }
        }

>>>>>>> Stashed changes
        private void slider_VolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
            {
                mediaElement.Volume = slider.Value;
            }
        }

<<<<<<< Updated upstream
=======
        private void SeekButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan newPosition = mediaElement.Position + TimeSpan.FromSeconds(10);
                mediaElement.Position = newPosition < mediaElement.NaturalDuration.TimeSpan ? newPosition : mediaElement.NaturalDuration.TimeSpan;
            }
        }

        private void SeekBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(e.NewValue - mediaElement.Position.TotalSeconds) > 1)
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }
>>>>>>> Stashed changes

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_songViewModel != null && _songViewModel.NextSongCommand.CanExecute(null))
            {
                _songViewModel.NextSongCommand.Execute(null);
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (_songViewModel != null && _songViewModel.PreviousSongCommand.CanExecute(null))
            {
                _songViewModel.PreviousSongCommand.Execute(null);
            }
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
=======
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = "C:\\Users",
                IsFolderPicker = true
            };
>>>>>>> Stashed changes
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
                _songViewModel = new SongViewModel(dialog.FileName);
                this.DataContext = _songViewModel;

                _songViewModel.PlaySongAction = (filePath) =>
                {
                    mediaElement.Source = new Uri(filePath, UriKind.RelativeOrAbsolute);
                    mediaElement.Play();
                    mediaElement.MediaEnded += MediaElement_MediaEnded;
<<<<<<< Updated upstream
=======
                    TotalDurationText.Text = mediaElement.NaturalDuration.HasTimeSpan
                                              ? mediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss")
                                              : "0:00";
                    _timer.Start(); // Start timer for the new song
>>>>>>> Stashed changes
                };
            }
        }

        private void OpenMusicPlayer_Click(object sender, RoutedEventArgs e)
        {
            _songViewModel.ShowVideoView();
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
