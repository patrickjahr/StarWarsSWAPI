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
    public class PlanetRepository : IPlanetRepository
    {
        private readonly IDataService _dataService;
        private readonly ILocalStorageService _localStorageService;
        private readonly ObservableCollection<Planet> _currentPlanets = new ObservableCollection<Planet>(); 

        public PlanetRepository(IDataService dataService, ILocalStorageService localStorageService)
        {
            _dataService = dataService;
            _localStorageService = localStorageService;
        }

        public async Task<ObservableCollection<Planet>> GetAllPlanetsAsync(bool refresh = false)
        {
            if (refresh)
            {
                _currentPlanets.Clear();
                await LoadDataAndSaveFileAsync();
            }
            else if (!_currentPlanets.HasItems())
            {
                var file = await _localStorageService.ReadStringFromStorageAsync("planets");
                if (file.IsNeitherNullNorEmpty())
                {
                    var planets = JsonConvert.DeserializeObject<ObservableCollection<Planet>>(file);
                    _currentPlanets.AddRange(planets);
                }
                else
                {
                    await LoadDataAndSaveFileAsync();
                }
            }
            return _currentPlanets;
        }

        public async Task<ObservableCollection<Planet>> SearchPlanets(string searchText)
        {
            if (!_currentPlanets.HasItems())
                await LoadDataAndSaveFileAsync();

            return _currentPlanets.HasItems() ? _currentPlanets.Where(item => item.name.ContainsIgnoreCase(searchText)).ToObservableCollection() : new ObservableCollection<Planet>();
        }

        private async Task LoadDataAndSaveFileAsync()
        {
            var result = await _dataService.GetAllPlanets();
            _currentPlanets.AddRange(result.results);
            while (!String.IsNullOrEmpty(result.nextPageNo))
            {
                result = await _dataService.GetAllPlanets(result.nextPageNo);
                _currentPlanets.AddRange(result.results);
            }

            var resultString = JsonConvert.SerializeObject(_currentPlanets);
            await _localStorageService.WriteStringToStorageAsync("planets", resultString);
        }

        public async Task<Planet> GetPlanetAsync(string id)
        {
            var currentPlanet = new Planet();
            if (!id.IsNeitherNullNorEmpty()) return currentPlanet;
            if (_currentPlanets.HasItems())
            {
                var planet = _currentPlanets.FirstOrDefault(item => item.url == id);
                if (planet != null)
                    return planet;
            }
            var result = await _dataService.GetPlanet(id);
            if (result != null)
                currentPlanet = result;

            return currentPlanet;
        }
    }
}