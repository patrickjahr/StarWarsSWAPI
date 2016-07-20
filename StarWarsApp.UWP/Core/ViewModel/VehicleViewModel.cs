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
    public class VehicleViewModel : MainViewModel
    {
        private readonly IDataService _dataService;
        private readonly IPlanetRepository _planetRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IStarshipRepository _starshipRepository;
        private readonly ISpeciesRepository _speciesRepository;
        
        private Vehicle _vehicle;
        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value; RaisePropertyChanged(); }
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

        public AsyncRelayCommand<Vehicle> InitAsyncCommand { get; private set; }

        public VehicleViewModel(IDataService dataService, IFilmRepository filmRepository, IPeopleRepository peopleRepository)
        {
            _dataService = dataService;
            _filmRepository = filmRepository;
            _peopleRepository = peopleRepository;
            InitAsyncCommand = new AsyncRelayCommand<Vehicle>(InitAsync);
        }

        private async Task InitAsync(Vehicle vehicle)
        {
            IsLoading = true;

            if (vehicle != null)
            {
                Vehicle = vehicle;

                Movies.Clear();
                foreach (var film in vehicle.films)
                {
                    var currentFilm = await _filmRepository.GetFilmAsync(film) ??
                                         await _dataService.GetSingleByUrl<Film>(film);
                    Movies.Add(currentFilm);
                }
                Movies = Movies.OrderBy(item => item.episode_id).ToObservableCollection();

                People.Clear();
                foreach (var person in vehicle.pilots)
                {
                    var currentPerson = await _peopleRepository.GetPeopleAsync(person) ??
                                         await _dataService.GetSingleByUrl<People>(person);
                    People.Add(currentPerson);
                }
                People = People.OrderBy(item => item.name).ToObservableCollection();
            }
            IsLoading = false;
        }
    }
}