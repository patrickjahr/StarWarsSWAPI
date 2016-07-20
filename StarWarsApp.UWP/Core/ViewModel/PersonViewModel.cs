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
    public class PersonViewModel : MainViewModel
    {
        private readonly IDataService _dataService;
        private readonly IPlanetRepository _planetRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private People _person;
        public People Person
        {
            get { return _person; }
            set { _person = value; RaisePropertyChanged(); }
        }

        private Planet _homeWorld;
        public Planet HomeWorld
        {
            get { return _homeWorld; }
            set { _homeWorld = value; RaisePropertyChanged(); }
        }

        private Specie _currentSpecie;
        public Specie CurrentSpecie
        {
            get { return _currentSpecie; }
            set { _currentSpecie = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles ?? (_vehicles = new ObservableCollection<Vehicle>()); }
            set { _vehicles = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Film> _movies;
        public ObservableCollection<Film> Movies
        {
            get { return _movies ?? (_movies ?? new ObservableCollection<Film>()); }
            set { _movies = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Starship> _starships;
        public ObservableCollection<Starship> Starships
        {
            get { return _starships ?? (_starships = new ObservableCollection<Starship>()); }
            set { _starships = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Specie> _species;
        public ObservableCollection<Specie> Species
        {
            get { return _species ?? (_species = new ObservableCollection<Specie>()); }
            set { _species = value; RaisePropertyChanged(); }
        }

        public AsyncRelayCommand<People> InitAsyncCommand { get; private set; }

        public PersonViewModel(IDataService dataService, IPlanetRepository planetRepository, IVehiclesRepository vehiclesRepository, IFilmRepository filmRepository, IStarshipRepository starshipRepository, ISpeciesRepository speciesRepository)
        {
            _dataService = dataService;
            _planetRepository = planetRepository;
            _vehiclesRepository = vehiclesRepository;
            _filmRepository = filmRepository;
            _starshipRepository = starshipRepository;
            _speciesRepository = speciesRepository;
            InitAsyncCommand = new AsyncRelayCommand<People>(InitAsync);
        }

        private async Task InitAsync(People person)
        {
            IsLoading = true;

            if (person != null)
            {
                Person = person;

                Species.Clear();
                if (person.species != null && person.species.Count() > 1)
                {
                    
                    foreach (var specy in person.species)
                    {
                        var currentSpecy = await _speciesRepository.GetSpecieAsync(specy) ??
                                           await _dataService.GetSingleByUrl<Specie>(specy);
                        Species.Add(currentSpecy);
                    }
                    Species = Species.OrderBy(item => item.name).ToObservableCollection();
                }
                else
                {
                    var specy = person.species.FirstOrDefault();
                    if(specy != null)
                        CurrentSpecie = await _speciesRepository.GetSpecieAsync(specy) ??
                                        await _dataService.GetSingleByUrl<Specie>(specy);
                }

                Starships.Clear();
                foreach (var starship in person.starships)
                {
                    var currentStarship = await _starshipRepository.GetStarshipAsync(starship) ??
                                          await _dataService.GetSingleByUrl<Starship>(starship);
                    Starships.Add(currentStarship);
                }
                Starships = Starships.OrderBy(item => item.name).ToObservableCollection();

                Vehicles.Clear();
                foreach (var vehicle in person.vehicles)
                {
                    var currentVehicle = await _vehiclesRepository.GetVehicleAsync(vehicle) ??
                                         await _dataService.GetSingleByUrl<Vehicle>(vehicle);
                    Vehicles.Add(currentVehicle);
                }
                Vehicles = Vehicles.OrderBy(item => item.name).ToObservableCollection();

                var movies = new ObservableCollection<Film>();
                foreach (var film in person.films)
                {
                    var currentFilm = await _filmRepository.GetFilmAsync(film) ??
                                         await _dataService.GetSingleByUrl<Film>(film);
                    movies.Add(currentFilm);
                }
                Movies = movies.OrderBy(item => item.episode_id).ToObservableCollection();

                HomeWorld = await _planetRepository.GetPlanetAsync(Person?.homeworld) ??
                            await _dataService.GetSingleByUrl<Planet>(Person?.homeworld);
            }
            IsLoading = false;
        }
    }
}