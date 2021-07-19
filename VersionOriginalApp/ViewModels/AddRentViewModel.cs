using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using VersionOriginalApp.Annotations;
using VersionOriginalApp.Domain.Dtos;
using VersionOriginalApp.Services.Interfaces;

namespace VersionOriginalApp.ViewModels
{
    public class AddRentViewModel : INotifyPropertyChanged
    {
        private readonly IVersionOriginalApiService _versionOriginalApiService;
        private bool _loading;

        private bool _searchOption;
        public bool SearchOption {
            get => _searchOption;
            set {
                _searchOption = value;
                ShowDniAutoComplete = _searchOption ? Visibility.Visible : Visibility.Hidden;
                ShowClientAutoComplete = _searchOption ? Visibility.Hidden : Visibility.Visible;
                NotifyPropertyChanged(nameof(SearchOption));
            }
        }

        private bool _dvdOption;
        public bool DvdOption {
            get => _dvdOption;
            set {
                _dvdOption = value;
                ShowFilmAutoComplete = _dvdOption ? Visibility.Visible : Visibility.Hidden;
                ShowGameAutoComplete = _dvdOption ? Visibility.Hidden : Visibility.Visible;
                NotifyPropertyChanged(nameof(DvdOption));
            }
        }

        private Visibility _showDniAutoComplete;
        public Visibility ShowDniAutoComplete {
            get => _showDniAutoComplete;
            set {
                _showDniAutoComplete = value;
                NotifyPropertyChanged(nameof(ShowDniAutoComplete));
            }
        }

        private Visibility _showClientAutoComplete;
        public Visibility ShowClientAutoComplete {
            get => _showClientAutoComplete;
            set {
                _showClientAutoComplete = value;
                NotifyPropertyChanged(nameof(ShowClientAutoComplete));
            }
        }

        private Visibility _showFilmAutoComplete;
        public Visibility ShowFilmAutoComplete {
            get => _showFilmAutoComplete;
            set {
                _showFilmAutoComplete = value;
                NotifyPropertyChanged(nameof(ShowFilmAutoComplete));
            }
        }

        private Visibility _showGameAutoComplete;
        public Visibility ShowGameAutoComplete {
            get => _showGameAutoComplete;
            set {
                _showGameAutoComplete = value;
                NotifyPropertyChanged(nameof(ShowGameAutoComplete));
            }
        }

        public bool Loading {
            get => _loading;

            set {
                _loading = value;
                NotifyPropertyChanged(nameof(Loading));
            }
        }

        private IEnumerable<ClientDto> _clients;
        public IEnumerable<ClientDto> Clients {
            get => _clients;
            set {
                _clients = value;
                NotifyPropertyChanged(nameof(Clients));
            }
        }

        private ClientDto _selectedClient;
        public ClientDto SelectedClient {
            get => _selectedClient;

            set {
                _selectedClient = value;
                NotifyPropertyChanged(nameof(SelectedClient));
            }
        }

        private IEnumerable<FilmDvdDto> _films;
        public IEnumerable<FilmDvdDto> Films {
            get => _films;
            set {
                _films = value;
                NotifyPropertyChanged(nameof(Films));
            }
        }

        private FilmDvdDto _selectedFilm;
        public FilmDvdDto SelectedFilm {
            get => _selectedFilm;

            set {
                _selectedFilm = value;
                NotifyPropertyChanged(nameof(SelectedFilm));
            }
        }

        private IEnumerable<GameDvdDto> _games;
        public IEnumerable<GameDvdDto> Games {
            get => _games;
            set {
                _games = value;
                NotifyPropertyChanged(nameof(Games));
            }
        }

        private GameDvdDto _selectedGame;
        public GameDvdDto SelectedGame {
            get => _selectedGame;

            set {
                _selectedGame = value;
                NotifyPropertyChanged(nameof(SelectedGame));
            }
        }

        private Visibility _showForm;
        public Visibility ShowForm {
            get => _showForm;
            set {
                _showForm = value;
                NotifyPropertyChanged(nameof(ShowForm));
            }
        }

        private IEnumerable<DayItem> _days;
        public IEnumerable<DayItem> Days {
            get => new List<DayItem> {
                new DayItem {
                    Id = 1,
                    Name = "1"
                },
                new DayItem {
                    Id = 2,
                    Name = "2"
                },
                new DayItem {
                    Id = 3,
                    Name = "3"
                },
                new DayItem {
                    Id = 4,
                    Name = "4"
                },
                new DayItem {
                    Id = 5,
                    Name = "5"
                }
            };
            set {
                _days = value;
                NotifyPropertyChanged(nameof(Days));
            }
        }

        private DayItem _selectNumberOfDays;
        public DayItem SelectNumberOfDays {
            get => _selectNumberOfDays;
            set {
                _selectNumberOfDays = value;
                NotifyPropertyChanged(nameof(SelectNumberOfDays));
            }
        }

        public AddRentViewModel(IVersionOriginalApiService versionOriginalApiService)
        {
            _versionOriginalApiService = versionOriginalApiService;
            _loading = true;
            _showForm = Visibility.Hidden;
            SearchOption = true;
            DvdOption = true;
        }

        public async Task LoadClients()
        {
            Loading = true;
            ShowForm = Visibility.Hidden;
            var result = await _versionOriginalApiService.GetClients(new PaginateParametersDto {
                CurrentPage = 1,
                ItemByPage = 2000
            }, new ClientStatusDto {
                Name = "",
                Id = Guid.Parse("dc4a5cad-775a-4e9c-9cf3-e6dc3f515d6b")
            });
            Clients = result.Data;
            Loading = false;
        }

        public async Task LoadFilms()
        {
            Loading = true;
            var result = await _versionOriginalApiService.GetFilmDvds(new PaginateParametersDto {
                CurrentPage = 1,
                ItemByPage = 1000
            }, new DvdStatusDto {
                Name = "",
                Id = Guid.Parse("c0de22e2-e33c-42cf-8b31-9bb7b94c45e8")
            });
            Films = result.Data;
            Loading = false;
        }

        public async Task LoadGames()
        {
            Loading = true;
            var result = await _versionOriginalApiService.GetGameDvds(new PaginateParametersDto {
                CurrentPage = 1,
                ItemByPage = 1000
            }, new DvdStatusDto {
                Name = "",
                Id = Guid.Parse("c0de22e2-e33c-42cf-8b31-9bb7b94c45e8")
            });
            Games = result.Data;
            ShowForm = Visibility.Visible;
            Loading = false;
        }

        public async Task AddRent()
        {
            if (_selectedClient == null) {
                MessageBox.Show("Debe seleccionar un cliente.");
                return;
            }

            if (_dvdOption && _selectedFilm == null) {
                MessageBox.Show("Debe seleccionar una película.");
                return;
            }

            if (!_dvdOption && _selectedGame == null) {
                MessageBox.Show("Debe seleccionar un video juego.");
                return;
            }

            var newRent = new RentDto {
                ClientId = _selectedClient.Id,
                Discount = 0,
                Amount = _dvdOption
                    ? _selectedFilm.RentPrice * _selectNumberOfDays.Id
                    : _selectedGame.RentPrice * _selectNumberOfDays.Id,
                DvdId = _dvdOption
                    ? _selectedFilm.Id
                    : _selectedGame.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(_selectNumberOfDays.Id),
                Code = "-",
                ReturnedDate = DateTime.MinValue,
                OutOfDateAmount = 0
            };

            await _versionOriginalApiService.AddRent(newRent);

            MessageBox.Show("La renta se adicionado con exito.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
