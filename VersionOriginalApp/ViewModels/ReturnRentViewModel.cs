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
    public class ReturnRentViewModel : INotifyPropertyChanged
    {
        private readonly IVersionOriginalApiService _versionOriginalApiService;
        private bool _loading;
        private Visibility _showForm;

        public ReturnRentViewModel(IVersionOriginalApiService versionOriginalApiService)
        {
            _versionOriginalApiService = versionOriginalApiService;
            _loading = false;
            _showForm = Visibility.Visible;
        }

        public Visibility ShowForm {
            get => _showForm;

            set
            {
                _showForm = value;
                NotifyPropertyChanged(nameof(ShowForm));
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

        public async Task<ReturnDto> MakeReturn(string code)
        {
            Loading = true;
            ShowForm = Visibility.Hidden;
            var result = await _versionOriginalApiService.ReturnRent(code);
            ShowForm = Visibility.Visible;
            Loading = false;
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}