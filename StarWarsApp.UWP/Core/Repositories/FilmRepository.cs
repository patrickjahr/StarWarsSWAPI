using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Core.Services;
using Core.Utils;
using Core.Utils.Extentions;
using Model;
using Newtonsoft.Json;

namespace Core.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly IDataService _dataService;
        private readonly ILocalStorageService _localStorageService;
        private ObservableCollection<Film> _currentFilms = new ObservableCollection<Film>();

        public FilmRepository(IDataService dataService, ILocalStorageService localStrStorageService)
        {
            _dataService = dataService;
            _localStorageService = localStrStorageService;
        }

        public async Task<ObservableCollection<Film>> GetAllFilms(bool refresh = false)
        {
            if (refresh)
            {
                _currentFilms.Clear();
                await LoadDataAndSaveFileAsync();
            }
            else if (!_currentFilms.HasItems())
            {
                var file = await _localStorageService.ReadStringFromStorageAsync("films");
                if (file.IsNeitherNullNorEmpty())
                {
                    var films = JsonConvert.DeserializeObject<ObservableCollection<Film>>(file);
                    _currentFilms.AddRange(films);
                }
                else
                {
                    await LoadDataAndSaveFileAsync();
                }
            }
            return _currentFilms;
        }

        public async Task<ObservableCollection<Film>> SearchFilms(string searchText)
        {
            if(!_currentFilms.HasItems())
                await LoadDataAndSaveFileAsync();

            return _currentFilms.HasItems() ? _currentFilms.Where(item => item.title.ContainsIgnoreCase(searchText)).ToObservableCollection() : new ObservableCollection<Film>();
        }

        private async Task LoadDataAndSaveFileAsync()
        {
            var result = await _dataService.GetAllFilms();
            _currentFilms.AddRange(result.results);
            while (!String.IsNullOrEmpty(result.nextPageNo))
            {
                result = await _dataService.GetAllFilms(result.nextPageNo);
                _currentFilms.AddRange(result.results);
            }
            _currentFilms = _currentFilms.OrderBy(item => item.episode_id).ToObservableCollection();

            var resultString = JsonConvert.SerializeObject(_currentFilms);
            await _localStorageService.WriteStringToStorageAsync("films", resultString);
        }

        public async Task<Film> GetFilmAsync(string id)
        {
            var currentFilm = new Film();
            if (_currentFilms.HasItems())
            {
                var film = _currentFilms.FirstOrDefault(item => item.url == id);
                if (film != null)
                    return film;
            }
            var result = await _dataService.GetFilm(id);
            if (result != null)
                currentFilm = result;

            return currentFilm;
        }
    }
}
