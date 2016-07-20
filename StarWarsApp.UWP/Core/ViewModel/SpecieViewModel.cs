using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Repositories;
using Core.Services;
using Core.Utils.Commands;
using Core.Utils.Extentions;
using Model;

namespace Core.ViewModel
{
    public class SpecieViewModel : MainViewModel
    {
        private readonly IDataService _dataService;
        private readonly IPlanetRepository _planetRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly ISpeciesRepository _speciesRepository;

        private Specie _specie;
        public Specie Specie
        {
            get { return _specie; }
            set { _specie = value; RaisePropertyChanged(); }
        }

        private Planet _homeWorld;
        public Planet HomeWorld
        {
            get { return _homeWorld; }
            set { _homeWorld = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Film> _movies;
        public ObservableCollection<Film> Movies
        {
            get { return _movies ?? (_movies ?? new ObservableCollection<Film>()); }
            set { _movies = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<People> _people;
        public ObservableCollection<People> People
        {
            get { return _people ?? (_people = new ObservableCollection<People>()); }
            set { _people = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles ?? (_vehicles = new ObservableCollection<Vehicle>()); }
            set { _vehicles = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Starship> _starships;
        public ObservableCollection<Starship> Starships
        {
            get { return _starships ?? (_starships = new ObservableCollection<Starship>()); }
            set { _starships = value; RaisePropertyChanged(); }
        }

        public AsyncRelayCommand<Specie> InitAsyncCommand { get; private set; }

        public SpecieViewModel(IDataService dataService, IFilmRepository filmRepository, IPeopleRepository peopleRepository, IVehiclesRepository vehiclesRepository, IStarshipRepository starshipRepository, IPlanetRepository planetRepository)
        {
            _dataService = dataService;
            _filmRepository = filmRepository;
            _peopleRepository = peopleRepository;
            _vehiclesRepository = vehiclesRepository;
            _starshipRepository = starshipRepository;
            _planetRepository = planetRepository;
            InitAsyncCommand = new AsyncRelayCommand<Specie>(InitAsync);
        }

        private async Task InitAsync(Specie specie)
        {
            IsLoading = true;

            if (specie != null)
            {
                Specie = specie;

                Movies.Clear();
                if (specie.films.HasItems())
                {
                    foreach (var film in specie.films)
                    {
                        var currentFilm = await _filmRepository.GetFilmAsync(film) ??
                                          await _dataService.GetSingleByUrl<Film>(film);
                        Movies.Add(currentFilm);
                    }
                    Movies = Movies.OrderBy(item => item.episode_id).ToObservableCollection();
                }
                People.Clear();
                if (specie.people.HasItems())
                {
                    foreach (var person in specie.people)
                    {
                        var currentPerson = await _peopleRepository.GetPeopleAsync(person) ??
                                            await _dataService.GetSingleByUrl<People>(person);
                        People.Add(currentPerson);
                    }
                    People = People.OrderBy(item => item.name).ToObservableCollection();
                }
                Starships.Clear();
                if (specie.starships.HasItems())
                {
                    foreach (var starship in specie.starships)
                    {
                        var currentStarship = await _starshipRepository.GetStarshipAsync(starship) ??
                                              await _dataService.GetSingleByUrl<Starship>(starship);
                        Starships.Add(currentStarship);
                    }
                    Starships = Starships.OrderBy(item => item.name).ToObservableCollection();
                }
                Vehicles.Clear();
                if (specie.vehicles.HasItems())
                {
                    foreach (var vehicle in specie.vehicles)
                    {
                        var currentVehicle = await _vehiclesRepository.GetVehicleAsync(vehicle) ??
                                             await _dataService.GetSingleByUrl<Vehicle>(vehicle);
                        Vehicles.Add(currentVehicle);
                    }
                    Vehicles = Vehicles.OrderBy(item => item.name).ToObservableCollection();
                }

                if (specie.homeworld.IsNeitherNullNorEmpty())
                {
                    HomeWorld = await _planetRepository.GetPlanetAsync(Specie?.homeworld) ??
                                await _dataService.GetSingleByUrl<Planet>(Specie?.homeworld);
                }
            }
            IsLoading = false;
        }
    }
}