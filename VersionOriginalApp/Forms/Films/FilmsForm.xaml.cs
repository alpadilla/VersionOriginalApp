using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using VersionOriginalApp.Domain.Dtos;
using VersionOriginalApp.Services.Interfaces;
using VersionOriginalApp.ViewModels;

namespace VersionOriginalApp.Forms.Films
{
    /// <summary>
    /// Interaction logic for FilmForms.xaml
    /// </summary>
    public partial class FilmForms : Window
    {
        public FilmForms(IVersionOriginalApiService versionOriginalApiService)
        {
            InitializeComponent();
            DataContext = new FilmListViewModel(versionOriginalApiService);
        }

        private async void FilmForms_OnContentRendered(object? sender, EventArgs e)
        {
            //var dvdsStatusItems = await ((FilmListViewModel) DataContext).LoadDvdsStatus();
            //dvdsStatusComboBox.ItemsSource = dvdsStatusItems;
            //dvdsStatusComboBox.DisplayMemberPath = "Name";
            //dvdsStatusComboBox.SelectedValuePath = "Id";
            //dvdsStatusComboBox.SelectedIndex = 0;
            await ((FilmListViewModel) DataContext).LoadDvdsStatus();
            await ((FilmListViewModel) DataContext).LoadFilms(LoadFilmDvdActions.Default);
        }

        private async void BtnFirst_OnClick(object sender, RoutedEventArgs e)
        {
            await ((FilmListViewModel) DataContext).LoadFilms(LoadFilmDvdActions.FirstPage);
        }

        private async void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            await ((FilmListViewModel) DataContext).LoadFilms(LoadFilmDvdActions.NextPage);
        }

        private async void BtnLast_OnClick(object sender, RoutedEventArgs e)
        {
            await ((FilmListViewModel) DataContext).LoadFilms(LoadFilmDvdActions.LastPage);
        }

        private async void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            await ((FilmListViewModel) DataContext).LoadFilms(LoadFilmDvdActions.PreviousPage);
        }

        private async void DvdsStatusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dvdStatus = (sender as ComboBox)?.SelectedItem as DvdStatusDto;
            if (dvdStatus != null) {
                ((FilmListViewModel) DataContext).SelectDvdStatus = dvdStatus;
                await((FilmListViewModel) DataContext).LoadFilms(LoadFilmDvdActions.Default);
            }
        }
    }
}
