using System.Threading.Tasks;
using System.Windows;
using VersionOriginalApp.Forms.Clients;
using VersionOriginalApp.Forms.Films;
using VersionOriginalApp.Services;
using VersionOriginalApp.Services.Interfaces;

namespace VersionOriginalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IVersionOriginalApiService _versionOriginalApiService;
        public MainWindow(IVersionOriginalApiService versionOriginalApiService)
        {
            _versionOriginalApiService = versionOriginalApiService;
            InitializeComponent();
        }

        private async void FilmListItem_OnClick(object sender, RoutedEventArgs e)
        {
            var filmListWindows = new FilmForms(_versionOriginalApiService);
            filmListWindows.Show();
        }

        private void Clients_OnClick(object sender, RoutedEventArgs e)
        {
            var clientsForm = new ClientsForm(_versionOriginalApiService);
            clientsForm.Show();
        }
    }
}
