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
    public class PlanetViewModel : MainViewModel
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IDataService _dataService;
        private Planet _planet;
        public Planet Planet
        {
            get { return _planet; }
            set { _planet = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Film> _movies;
        public ObservableCollection<Film> Movies
        {
            get { return _movies ?? (_movies = new ObservableCollection<Film>()); }
            set { _movies = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<People> _people;
        public ObservableCollection<People> People
        {
            get { return _people ?? (_people = new ObservableCollection<People>()); }
            set { _people = value; RaisePropertyChanged(); }
        }

        public AsyncRelayCommand<Planet> InitAsyncCommand { get; private set; }

        public PlanetViewModel(IPeopleRepository peopleRepository, IFilmRepository filmRepository, IDataService dataService)
        {
            _peopleRepository = peopleRepository;
            _filmRepository = filmRepository;
            _dataService = dataService;
            InitAsyncCommand = new AsyncRelayCommand<Planet>(InitAsync);
            
        }

        private async Task InitAsync(Planet arg)
        {
            IsLoading = true;
            if (arg != null)
            {
                Planet = arg;
                Movies.Clear();
                foreach (var film in Planet.films)
                {
                    var currentFilm = await _filmRepository.GetFilmAsync(film) ??
                                      await _dataService.GetSingleByUrl<Film>(film);
                    Movies.Add(currentFilm);
                }
                Movies = Movies.OrderBy(item => item.episode_id).ToObservableCollection();

                People.Clear();
                foreach (var person in Planet.residents)
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