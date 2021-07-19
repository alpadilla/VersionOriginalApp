using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VersionOriginalApp.Domain.Dtos;
using VersionOriginalApp.Services.Interfaces;
using VersionOriginalApp.ViewModels;

namespace VersionOriginalApp.Forms.Rents
{
    /// <summary>
    /// Interaction logic for AddRentFrom.xaml
    /// </summary>
    public partial class AddRentFrom : Window
    {
        public AddRentFrom(IVersionOriginalApiService versionOriginalApiService)
        {
            InitializeComponent();
            DataContext = new AddRentViewModel(versionOriginalApiService);
        }

        private async void AddRentFrom_OnContentRendered(object? sender, EventArgs e)
        {
            await ((AddRentViewModel) DataContext).LoadClients();
            await ((AddRentViewModel) DataContext).LoadFilms();
            await ((AddRentViewModel) DataContext).LoadGames();
        }

        private async void AddRent_OnClick(object sender, RoutedEventArgs e)
        {
            await ((AddRentViewModel) DataContext).AddRent();
            Close();
        }

        private void CancelAddRent_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DaysComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedNumberOfDay = (sender as ComboBox)?.SelectedItem as DayItem;
            if (selectedNumberOfDay != null) {
                ((AddRentViewModel) DataContext).SelectNumberOfDays = selectedNumberOfDay;
            }
        }
    }
}
