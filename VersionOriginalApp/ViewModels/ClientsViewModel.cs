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
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private readonly IVersionOriginalApiService _versionOriginalApiService;
        private bool _loading;
        private Visibility _showClients;
        private int _currentPage;
        private int _numberOfPages;
        private int _itemByPage;
        private int _totalOfItems;
        private string _paginateInfo;
        private ClientStatusDto _selectedClientStatus;
        private List<ClientStatusDto> _clientsStatus;

        public ClientsViewModel(IVersionOriginalApiService versionOriginalApiService)
        {
            _versionOriginalApiService = versionOriginalApiService;
            _loading = true;
            _showClients = Visibility.Hidden;
            _currentPage = 1;
            _itemByPage = 14;
            _numberOfPages = 0;
            _totalOfItems = 0;
        }

        public Visibility ShowClients
        {
            get => _showClients;

            set
            {
                _showClients = value;
                NotifyPropertyChanged(nameof(ShowClients));
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

        public int CurrentPage {
            get => _currentPage;

            set {
                _currentPage = value;
                NotifyPropertyChanged(nameof(CurrentPage));
            }
        }

        public int NumberOfPages {
            get => _numberOfPages;

            set {
                _numberOfPages = value;
                NotifyPropertyChanged(nameof(NumberOfPages));
            }
        }

        public int TotalOfItems {
            get => _totalOfItems;

            set {
                _totalOfItems = value;
                NotifyPropertyChanged(nameof(TotalOfItems));
            }
        }
        public string PaginateInfo {
            get => _paginateInfo;

            set {
                _paginateInfo = value;
                NotifyPropertyChanged(nameof(PaginateInfo));
            }
        }

        private IEnumerable<ClientDto> _clients;
        public IEnumerable<ClientDto> Clients
        { 
            get => _clients;
            set
            {
                _clients = value;
                NotifyPropertyChanged(nameof(Clients));
            }
        }

        public List<ClientStatusDto> ClientsStatus {
            get => _clientsStatus;
            set 
            {
                _clientsStatus = value;
                NotifyPropertyChanged(nameof(ClientsStatus));
            }
        }

        public ClientStatusDto SelectedClientStatus {
            get => _selectedClientStatus;

            set {
                _selectedClientStatus = value;
                NotifyPropertyChanged(nameof(SelectedClientStatus));
            }
        }

        public async Task LoadClients(PaginateAction action)
        {
            switch (action) {
                case PaginateAction.Default:
                    CurrentPage = 1;
                    break;
                case PaginateAction.LastPage:
                    CurrentPage = NumberOfPages;
                    break;
                case PaginateAction.NextPage:
                    CurrentPage += 1;
                    break;
                case PaginateAction.PreviousPage:
                    if (CurrentPage == 1) {
                        return;
                    }

                    CurrentPage -= 1;
                    break;
                case PaginateAction.FirstPage:
                    if (CurrentPage == 1) {
                        return;
                    }

                    CurrentPage = 1;
                    break;
            }
            Loading = true;
            ShowClients = Visibility.Hidden;
            var result = await _versionOriginalApiService.GetClients(new PaginateParametersDto
            {
                CurrentPage = _currentPage,
                ItemByPage = _itemByPage
            }, SelectedClientStatus);
            Clients = result.Data;
            NumberOfPages = result.TotalOfPages;
            TotalOfItems = result.TotalOfItems;
            PaginateInfo =
                $"Mostrando del {(_currentPage == 1 ? _currentPage : (_currentPage-1) * _itemByPage + 1)} al {_currentPage * _itemByPage} de {_totalOfItems} elementos";
            ShowClients = Visibility.Visible;
            Loading = false;
        }

        public async Task<IEnumerable<ClientStatusDto>> LoadClientsStatus()
        {
            Loading = true;
            ShowClients = Visibility.Hidden;
            var result = await _versionOriginalApiService.GetClientsStatus(new PaginateParametersDto {
                CurrentPage = _currentPage,
                ItemByPage = _itemByPage
            });
            ClientsStatus = result.Data.ToList();
            SelectedClientStatus = ClientsStatus.Any() ? ClientsStatus.First() : null;
            Loading = false;
            return result.Data;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}