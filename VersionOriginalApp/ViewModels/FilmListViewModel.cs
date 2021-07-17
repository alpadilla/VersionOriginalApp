using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class FilmListViewModel : INotifyPropertyChanged
    {
        private readonly IVersionOriginalApiService _versionOriginalApiService;
        private bool _loading;
        private Visibility _showFilms;
        private int _currentPage;
        private int _numberOfPages;
        private int _itemByPage;
        private int _totalOfItems;
        private string _paginateInfo;
        private DvdStatusDto _selectDvdStatus;
        private List<DvdStatusDto> _dvdStatus;

        public FilmListViewModel(IVersionOriginalApiService versionOriginalApiService)
        {
            _versionOriginalApiService = versionOriginalApiService;
            _loading = true;
            _showFilms = Visibility.Hidden;
            _currentPage = 1;
            _itemByPage = 14;
            _numberOfPages = 0;
            _totalOfItems = 0;
        }

        public Visibility ShowFilms
        {
            get => _showFilms;

            set
            {
                _showFilms = value;
                NotifyPropertyChanged(nameof(ShowFilms));
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

        private IEnumerable<FilmDvdDto> _filmDvds;
        public IEnumerable<FilmDvdDto> FilmDvds 
        { 
            get => _filmDvds;
            set
            {
                _filmDvds = value;
                NotifyPropertyChanged(nameof(FilmDvds));
            }
        }

        public List<DvdStatusDto> DvdStatus {
            get => _dvdStatus;
            set 
            {
                _dvdStatus = value;
                NotifyPropertyChanged(nameof(DvdStatus));
            }
        }

        public DvdStatusDto SelectDvdStatus {
            get => _selectDvdStatus;

            set {
                _selectDvdStatus = value;
                NotifyPropertyChanged(nameof(SelectDvdStatus));
            }
        }

        public async Task LoadFilms(LoadFilmDvdActions action)
        {
            switch (action) {
                case LoadFilmDvdActions.Default:
                    CurrentPage = 1;
                    break;
                case LoadFilmDvdActions.LastPage:
                    CurrentPage = NumberOfPages;
                    break;
                case LoadFilmDvdActions.NextPage:
                    CurrentPage += 1;
                    break;
                case LoadFilmDvdActions.PreviousPage:
                    if (CurrentPage == 1) {
                        return;
                    }

                    CurrentPage -= 1;
                    break;
                case LoadFilmDvdActions.FirstPage:
                    if (CurrentPage == 1) {
                        return;
                    }

                    CurrentPage = 1;
                    break;
            }
            Loading = true;
            ShowFilms = Visibility.Hidden;
            var result = await _versionOriginalApiService.GetFilmDvds(new PaginateParametersDto
            {
                CurrentPage = _currentPage,
                ItemByPage = _itemByPage
            }, SelectDvdStatus);
            FilmDvds = result.Data;
            NumberOfPages = result.TotalOfPages;
            TotalOfItems = result.TotalOfItems;
            PaginateInfo =
                $"Mostrando del {(_currentPage == 1 ? _currentPage : (_currentPage-1) * _itemByPage + 1)} al {_currentPage * _itemByPage} de {_totalOfItems} elementos";
            ShowFilms = Visibility.Visible;
            Loading = false;
        }

        public async Task<IEnumerable<DvdStatusDto>> LoadDvdsStatus()
        {
            Loading = true;
            ShowFilms = Visibility.Hidden;
            var result = await _versionOriginalApiService.GetDvdsStatus(new PaginateParametersDto {
                CurrentPage = _currentPage,
                ItemByPage = _itemByPage
            });
            DvdStatus = result.Data.ToList();
            SelectDvdStatus = DvdStatus.Any() ? DvdStatus.First() : null;
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
    
    public enum LoadFilmDvdActions
    {
        Default = 0,
        FirstPage = 1,
        LastPage = 2,
        PreviousPage = 3,
        NextPage = 4
    }
}
