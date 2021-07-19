using System;
using System.Windows;
using VersionOriginalApp.Services.Interfaces;
using VersionOriginalApp.ViewModels;

namespace VersionOriginalApp.Forms.Rents
{
    /// <summary>
    /// Interaction logic for RentSummaryForm.xaml
    /// </summary>
    public partial class RentSummaryForm : Window
    {
        public RentSummaryForm(IVersionOriginalApiService versionOriginalApiService)
        {
            InitializeComponent();
            DataContext = new RentSummaryViewModel(versionOriginalApiService);
        }

        private async void RentSummaryForm_OnContentRendered(object? sender, EventArgs e)
        {
            await ((RentSummaryViewModel) DataContext).LoadRentSummary();
        }
    }
}
