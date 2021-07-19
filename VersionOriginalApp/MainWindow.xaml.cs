using System.Threading.Tasks;
using System.Windows;
using VersionOriginalApp.Forms.Clients;
using VersionOriginalApp.Forms.Films;
using VersionOriginalApp.Forms.Rents;
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

        private void RentResume_OnClick(object sender, RoutedEventArgs e)
        {
            var rentResumeForm = new RentSummaryForm(_versionOriginalApiService);
            rentResumeForm.Show();
        }

        private void AddRent_OnClick(object sender, RoutedEventArgs e)
        {
            var addRentForm = new AddRentFrom(_versionOriginalApiService);
            addRentForm.Show();
        }

        private void MakeReturn_OnClick(object sender, RoutedEventArgs e)
        {
            var returnRentForm = new ReturnRentForm(_versionOriginalApiService);
            returnRentForm.Show();
        }

        private void CloseApp_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
