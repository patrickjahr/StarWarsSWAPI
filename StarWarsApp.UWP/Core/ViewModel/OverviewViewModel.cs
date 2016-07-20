using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Message;
using Core.Repositories;
using Core.Services;
using Core.Utils;
using Core.Utils.Commands;
using Core.Utils.Extentions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Model;

namespace Core.ViewModel
{
    public class OverviewViewModel : MainViewModel
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IPlanetRepository _planetRepository;
        private ObservableCollection<People> _people;
        private bool _isInit = false;
        private int _activeSearch;
        public ObservableCollection<People> People
        {
            get { return _people; }
            set { _people = value; RaisePropertyChanged(); }
        }

        private People _selectedPeople;
        public People SelectedPeople
        {
            get { return _selectedPeople; }
            set { _selectedPeople = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Film> _films;
        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set { _films = value; RaisePropertyChanged(); }
        }

        private Film _selectedFilm;
        public Film SelectedFilm
        {
            get { return _selectedFilm; }
            set { _selectedFilm = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Starship> _starships;
        public ObservableCollection<Starship> Starships
        {
            get { return _starships; }
            set { _starships = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set { _vehicles = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Specie> _species;
        public ObservableCollection<Specie> Species
        {
            get { return _species; }
            set { _species = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Planet> _planets;
        public ObservableCollection<Planet> Planets
        {
            get { return _planets; }
            set { _planets = value; RaisePropertyChanged(); }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; RaisePropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; RaisePropertyChanged(); }
        }

        private double _progressState;
        public double ProgressState
        {
            get { return _progressState; }
            set { _progressState = value; RaisePropertyChanged(); }
        }

        public AsyncRelayCommand InitCommand { get; private set; }
        public AsyncRelayCommand RefreshCommand { get; private set; }
        public AsyncRelayCommand SearchCommand { get; private set; }
        public AsyncRelayCommand ResetSearchCommand { get; private set; }

        public OverviewViewModel(IPeopleRepository peopleRepository, IFilmRepository filmRepository, IStarshipRepository starshipRepository, IVehiclesRepository vehiclesRepository, ISpeciesRepository speciesRepository, IPlanetRepository planetRepository)
        {
            _peopleRepository = peopleRepository;
            _filmRepository = filmRepository;
            _starshipRepository = starshipRepository;
            _vehiclesRepository = vehiclesRepository;
            _speciesRepository = speciesRepository;
            _planetRepository = planetRepository;
            InitCommand = new AsyncRelayCommand(Init);
            RefreshCommand = new AsyncRelayCommand(RefreshAsync);
            SearchCommand = new AsyncRelayCommand(SearchAsync);
            ResetSearchCommand = new AsyncRelayCommand(ResetSearchAsync);
        }

        private async Task ResetSearchAsync()
        {
            switch (_activeSearch)
            {
                case 0:
                    People = await _peopleRepository.GetAllPeopleAsync();
                    break;
                case 1:
                    Starships = await _starshipRepository.GetAllStarshipsAsync();
                    break;
                case 2:
                    Films = await _filmRepository.GetAllFilms();
                    break;
                case 3:
                    Planets = await _planetRepository.GetAllPlanetsAsync();
                    break;
                case 4:
                    Vehicles = await _vehiclesRepository.GetAllVehiclesAsync();
                    break;
                case 5:
                    Species = await _speciesRepository.GetAllSpeciesAsync();
                    break;

            }
        }


        private async Task SearchAsync()
        {
            bool searchPossible = SearchText.IsNeitherNullNorEmpty();

            switch (SelectedIndex)
            {
                case 0:
                    People = searchPossible
                        ? await _peopleRepository.SearchPeopleAsync(SearchText)
                        : await _peopleRepository.GetAllPeopleAsync();
                    _activeSearch = 0;
                    break;
                case 1:
                    Starships = searchPossible
                        ? await _starshipRepository.SearchStarships(SearchText)
                        : await _starshipRepository.GetAllStarshipsAsync();
                    _activeSearch = 1;
                    break;
                case 2:
                    Films = searchPossible
                        ? await _filmRepository.SearchFilms(SearchText)
                        : await _filmRepository.GetAllFilms();
                    _activeSearch = 2;
                    break;
                case 3:
                    Planets = searchPossible
                        ? await _planetRepository.SearchPlanets(SearchText)
                        : await _planetRepository.GetAllPlanetsAsync();
                    _activeSearch = 3;
                    break;
                case 4:
                    Vehicles = searchPossible
                        ? await _vehiclesRepository.SearchVehicles(SearchText)
                        : await _vehiclesRepository.GetAllVehiclesAsync();
                    _activeSearch = 4;
                    break;
                case 5:
                    Species = searchPossible
                        ? await _speciesRepository.SearchSpecies(SearchText)
                        : await _speciesRepository.GetAllSpeciesAsync();
                    _activeSearch = 5;
                    break;

            }

        }

        private async Task RefreshAsync()
        {
            IsLoading = true;
            await LoadDataAsync(true);
            IsLoading = false;
        }

        private async Task Init()
        {
            if (_isInit) return;
            IsLoading = true;
            await LoadDataAsync(false);
            IsLoading = false;
            Messenger.Default.Send(new AppInitCompletedMessage());
        }

        private async Task LoadDataAsync(bool refresh)
        {
            var step = 100 / 6;
            ProgressState = step * 1;
            People = await _peopleRepository.GetAllPeopleAsync(refresh);
            ProgressState = step * 2;
            Films = await _filmRepository.GetAllFilms(refresh);
            ProgressState = step * 3;
            Starships = await _starshipRepository.GetAllStarshipsAsync(refresh);
            ProgressState = step * 4;
            Species = await _speciesRepository.GetAllSpeciesAsync(refresh);
            ProgressState = step * 5;
            Vehicles = await _vehiclesRepository.GetAllVehiclesAsync(refresh);
            ProgressState = step * 6;
            Planets = await _planetRepository.GetAllPlanetsAsync(refresh);
            ProgressState = 100;
            await Task.Delay(500);
        }
    }
}