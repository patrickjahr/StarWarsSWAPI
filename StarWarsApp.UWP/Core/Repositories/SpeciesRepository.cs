using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Core.Utils.Extentions;
using GalaSoft.MvvmLight.Messaging;
using Model;
using Newtonsoft.Json;

namespace Core.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IDataService _dataService;
        private readonly ILocalStorageService _localStorageService;
        private readonly ObservableCollection<Specie> _currentSpecies = new ObservableCollection<Specie>(); 

        public SpeciesRepository(IDataService dataService, ILocalStorageService localStorageService)
        {
            _dataService = dataService;
            _localStorageService = localStorageService;
        }

        public async Task<ObservableCollection<Specie>> GetAllSpeciesAsync(bool refresh = false)
        {
            if (refresh)
            {
                _currentSpecies.Clear();
                await LoadDataAndSaveFileAsync();
            }
            else if (!_currentSpecies.HasItems())
            {
                var file = await _localStorageService.ReadStringFromStorageAsync("species");
                if (file.IsNeitherNullNorEmpty())
                {
                    var species = JsonConvert.DeserializeObject<ObservableCollection<Specie>>(file);
                    _currentSpecies.AddRange(species);
                }
                else
                {
                    await LoadDataAndSaveFileAsync();
                }
            }
            return _currentSpecies;
        }

        public async Task<ObservableCollection<Specie>> SearchSpecies(string searchText)
        {
            if (!_currentSpecies.HasItems())
                await LoadDataAndSaveFileAsync();

            return _currentSpecies.HasItems() ? _currentSpecies.Where(item => item.name.ContainsIgnoreCase(searchText)).ToObservableCollection() : new ObservableCollection<Specie>();
        }

        private async Task LoadDataAndSaveFileAsync()
        {
            var result = await _dataService.GetAllSpecies();
            _currentSpecies.AddRange(result.results);
            while (!String.IsNullOrEmpty(result.nextPageNo))
            {
                result = await _dataService.GetAllSpecies(result.nextPageNo);
                _currentSpecies.AddRange(result.results);
            }

            var resultString = JsonConvert.SerializeObject(_currentSpecies);
            await _localStorageService.WriteStringToStorageAsync("species", resultString);
        }

        public async Task<Specie> GetSpecieAsync(string id)
        {
            var currentSpecie = new Specie();
            if (_currentSpecies.HasItems())
            {
                var specy = _currentSpecies.FirstOrDefault(item => item.url == id);
                if (specy != null)
                    return specy;
            }
            var result = await _dataService.GetSpecie(id);
            if (result != null)
                currentSpecie = result;

            return currentSpecie;
        }
    }
}