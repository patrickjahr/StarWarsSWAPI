using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Repositories;
using Core.Services;
using Core.Utils.Commands;
using Core.Utils.Extentions;
using Model;

namespace Core.ViewModel
{
    public class MovieViewModel : MainViewModel
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IDataService _dataService;
        private Film _film;
        public Film Film
        {
            get { return _film; }
            set { _film = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles ?? (_vehicles = new ObservableCollection<Vehicle>()); }
            set { _vehicles = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Specie> _species;
        public ObservableCollection<Specie> Species
        {
            get { return _species ?? (_species = new ObservableCollection<Specie>()); }
            set { _species = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<People> _people;
        public ObservableCollection<People> People
        {
            get { return _people ?? (_people = new ObservableCollection<People>()); }
            set { _people = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Starship> _starships;
        public ObservableCollection<Starship> Starships
        {
            get { return _starships ?? (_starships = new ObservableCollection<Starship>()); }
            set { _starships = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Planet> _planets;
        public ObservableCollection<Planet> Planets
        {
            get { return _planets ?? (_planets = new ObservableCollection<Planet>()); }
            set { _planets = value; RaisePropertyChanged(); }
        }
        public AsyncRelayCommand<Film> InitAsyncCommand { get; private set; }

        public MovieViewModel(IVehiclesRepository vehiclesRepository, ISpeciesRepository speciesRepository, IPeopleRepository peopleRepository, IStarshipRepository starshipRepository, IPlanetRepository planetRepository, IDataService dataService)
        {
            _vehiclesRepository = vehiclesRepository;
            _speciesRepository = speciesRepository;
            _peopleRepository = peopleRepository;
            _starshipRepository = starshipRepository;
            _planetRepository = planetRepository;
            _dataService = dataService;
            InitAsyncCommand = new AsyncRelayCommand<Film>(InitAsync);
        }

        private async Task InitAsync(Film film)
        {
            IsLoading = true;
            if (film != null)
            {
                Film = film;

                Species.Clear();
                foreach (var specy in film.species)
                {
                    var currentSpecy = await _speciesRepository.GetSpecieAsync(specy) ??
                                       await _dataService.GetSingleByUrl<Specie>(specy);
                    Species.Add(currentSpecy);
                }
                Species = Species.OrderBy(item => item.name).ToObservableCollection();

                Starships.Clear();
                foreach (var starship in film.starships)
                {
                    var currentStarship = await _starshipRepository.GetStarshipAsync(starship) ??
                                          await _dataService.GetSingleByUrl<Starship>(starship);
                    Starships.Add(currentStarship);
                }
                Starships = Starships.OrderBy(item => item.name).ToObservableCollection();

                Vehicles.Clear();
                foreach (var vehicle in film.vehicles)
                {
                    var currentVehicle = await _vehiclesRepository.GetVehicleAsync(vehicle) ??
                                         await _dataService.GetSingleByUrl<Vehicle>(vehicle);
                    Vehicles.Add(currentVehicle);
                }
                Vehicles = Vehicles.OrderBy(item => item.name).ToObservableCollection();

                People.Clear();
                foreach (var character in film.characters)
                {
                    var currentPeople = await _peopleRepository.GetPeopleAsync(character) ??
                                        await _dataService.GetSingleByUrl<People>(character);
                    People.Add(currentPeople);
                }
                People = People.OrderBy(item => item.name).ToObservableCollection();

                Planets.Clear();
                foreach (var planet in film.planets)
                {
                    var currentPlanet = await _planetRepository.GetPlanetAsync(planet) ??
                                        await _dataService.GetSingleByUrl<Planet>(planet);
                    Planets.Add(currentPlanet);
                }
                Planets = Planets.OrderBy(item => item.name).ToObservableCollection();
            }
            IsLoading = false;
        }
    }
}