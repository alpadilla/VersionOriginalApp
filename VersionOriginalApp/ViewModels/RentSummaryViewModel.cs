using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using VersionOriginalApp.Annotations;
using VersionOriginalApp.Domain.Dtos;
using VersionOriginalApp.Services.Interfaces;

namespace VersionOriginalApp.ViewModels
{
    public class RentSummaryViewModel : INotifyPropertyChanged
    {
        private readonly IVersionOriginalApiService _versionOriginalApiService;
        private bool _loading;
        private Visibility _showRentSummary;

        public RentSummaryViewModel(IVersionOriginalApiService versionOriginalApiService)
        {
            _versionOriginalApiService = versionOriginalApiService;
            _loading = true;
            _showRentSummary = Visibility.Hidden;
        }

        public Visibility ShowRentSummary {
            get => _showRentSummary;

            set
            {
                _showRentSummary = value;
                NotifyPropertyChanged(nameof(ShowRentSummary));
            }
        }

        public bool Loading
        {
            get => _loading;

            set
            {
                _loading = value;
                NotifyPropertyChanged(nameof(Loading));
            }
        }

        private RentSummaryDto _rentSummary;

        private RentSummaryInfo _rentSummaryInfo;
        public RentSummaryInfo RentSummaryInfo { 
            get => _rentSummaryInfo;
            set
            {
                _rentSummaryInfo = value;
                NotifyPropertyChanged(nameof(RentSummaryInfo));
            }
        }

        public async Task LoadRentSummary()
        {
            Loading = true;
            ShowRentSummary = Visibility.Hidden;
            _rentSummary = await _versionOriginalApiService.GetRentSummary();
            RentSummaryInfo = new RentSummaryInfo {
                TotalRentsOfFilms = $"Total de películas rentadas: {_rentSummary.TotalRentsOfFilms}",
                TotalRentsOfGames = $"Total de video juegos rentados: {_rentSummary.TotalRentsOfGames}",
                ExpensesByYear = _rentSummary.ExpensesByYear
                    .Aggregate("Gastos por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                FilmsExpensesByYear = _rentSummary.FilmsExpensesByYear
                    .Aggregate("Gastos en películas por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                GamesExpensesByYear = _rentSummary.GamesExpensesByYear
                    .Aggregate("Gastos en video juegos por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                RevenuesByYear = _rentSummary.RevenuesByYear
                    .Aggregate("Ventas por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                FilmsRevenuesByYear = _rentSummary.FilmsRevenuesByYear
                    .Aggregate("Ventas en películas por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                GamesRevenuesByYear = _rentSummary.GamesRevenuesByYear
                    .Aggregate("Ventas en video juegos por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                FilmsRentsRevenuesAverageByYear = _rentSummary.FilmsRentsRevenuesAverageByYear
                    .Aggregate("Promedio de ventas de películas por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},"),
                GamesRentsRevenuesAverageByYear = _rentSummary.GamesRentsRevenuesAverageByYear
                    .Aggregate("Promedio de ventas de video juegos por año: ", (a, b) => a + $" {b.Year}: {Math.Round(b.Amount)},")
            };
            ShowRentSummary = Visibility.Visible;
            Loading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}