using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Core.Utils.Extentions;
using Model;
using Newtonsoft.Json;

namespace Core.Repositories
{
    public class StarshipRepository : IStarshipRepository
    {
        private readonly IDataService _dataService;
        private readonly ILocalStorageService _localStorageService;
        private ObservableCollection<Starship> _currentStarships = new ObservableCollection<Starship>(); 

        public StarshipRepository(IDataService dataService, ILocalStorageService localStorageService)
        {
            _dataService = dataService;
            _localStorageService = localStorageService;
        }

        public async Task<ObservableCollection<Starship>> GetAllStarshipsAsync(bool refresh = false)
        {
            if (refresh)
            {
                _currentStarships.Clear();
                await LoadDataAndSaveFileAsync();
            }
            else if (!_currentStarships.HasItems())
            {
                var file = await _localStorageService.ReadStringFromStorageAsync("starships");
                if (file.IsNeitherNullNorEmpty())
                {
                    var starships = JsonConvert.DeserializeObject<ObservableCollection<Starship>>(file);
                    _currentStarships.AddRange(starships);
                }
                else
                {
                    await LoadDataAndSaveFileAsync();
                }
            }
            return _currentStarships;
        }

        public async Task<ObservableCollection<Starship>> SearchStarships(string searchText)
        {
            if (!_currentStarships.HasItems())
                await LoadDataAndSaveFileAsync();

            return _currentStarships.HasItems() ? _currentStarships.Where(item => item.name.ContainsIgnoreCase(searchText)).ToObservableCollection() : new ObservableCollection<Starship>();
        }

        private async Task LoadDataAndSaveFileAsync()
        {
            var result = await _dataService.GetAllStarships();
            _currentStarships.AddRange(result.results);
            while (!String.IsNullOrEmpty(result.nextPageNo))
            {
                result = await _dataService.GetAllStarships(result.nextPageNo);
                _currentStarships.AddRange(result.results);
            }

            var resultString = JsonConvert.SerializeObject(_currentStarships);
            await _localStorageService.WriteStringToStorageAsync("starships", resultString);
        }

        public async Task<Starship> GetStarshipAsync(string id)
        {
            var currentStarship = new Starship();
            if (_currentStarships.HasItems())
            {
                var starship = _currentStarships.FirstOrDefault(item => item.url == id);
                if (starship != null)
                    return starship;
            }
            var result = await _dataService.GetStarship(id);
            if (result != null)
                currentStarship = result;

            return currentStarship;
        }
    }
}