﻿using MahApps.Metro.IconPacks;
using Music_Player.ViewModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace Music_Player
{

    public partial class MainWindow : Window
    {
        private bool isPlaying = true;
        private SongViewModel _songViewModel;

        public MainWindow()
        {
            InitializeComponent();

            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = @"C:\Users",
                IsFolderPicker = true 
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                _songViewModel = new SongViewModel(dialog.FileName);
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
            MessageBoxResult answer = MessageBox.Show("Do you really want to quit??", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
                mediaElement.Volume = slider.Value;
            }
        }


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
                    mediaElement.MediaEnded += MediaElement_MediaEnded;
                };
            }
        }

        private void OpenMusicPlayer_Click(object sender, RoutedEventArgs e)
        {
            _songViewModel.ShowVideoView();
        }
    }
}