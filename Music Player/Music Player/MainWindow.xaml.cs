using Music_Player.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Music_Player
{

    public partial class MainWindow : Window
    {
        private SongViewModel _songViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _songViewModel = new SongViewModel();
            this.DataContext = _songViewModel;

            _songViewModel.PlaySongAction = (filePath) =>
            {
                mediaElement.Source = new Uri(filePath, UriKind.RelativeOrAbsolute);
                mediaElement.Play();
            };
        }
    }
}