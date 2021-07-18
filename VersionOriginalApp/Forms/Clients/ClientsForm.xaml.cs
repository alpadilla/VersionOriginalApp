using System;
using System.Windows;
using System.Windows.Controls;
using VersionOriginalApp.Domain.Dtos;
using VersionOriginalApp.Services.Interfaces;
using VersionOriginalApp.ViewModels;

namespace VersionOriginalApp.Forms.Clients
{
    /// <summary>
    /// Interaction logic for ClientsForm.xaml
    /// </summary>
    public partial class ClientsForm : Window
    {
        public ClientsForm(IVersionOriginalApiService versionOriginalApiService)
        {
            InitializeComponent();
            DataContext = new ClientsViewModel(versionOriginalApiService);
        }

        private async void ClientsForm_OnContentRendered(object? sender, EventArgs e)
        {
            await ((ClientsViewModel) DataContext).LoadClientsStatus();
            await ((ClientsViewModel) DataContext).LoadClients(PaginateAction.Default);
        }

        private async void BtnFirst_OnClick(object sender, RoutedEventArgs e)
        {
            await ((ClientsViewModel) DataContext).LoadClients(PaginateAction.FirstPage);
        }

        private async void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            await ((ClientsViewModel) DataContext).LoadClients(PaginateAction.NextPage);
        }

        private async void BtnLast_OnClick(object sender, RoutedEventArgs e)
        {
            await ((ClientsViewModel) DataContext).LoadClients(PaginateAction.LastPage);
        }

        private async void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            await ((ClientsViewModel) DataContext).LoadClients(PaginateAction.PreviousPage);
        }

        private async void ClientsStatusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox)?.SelectedItem is not ClientStatusDto clientStatus) {
                return;
            }

            ((ClientsViewModel) DataContext).SelectedClientStatus = clientStatus;
            await ((ClientsViewModel) DataContext).LoadClients(PaginateAction.Default);
        }
    }
}
