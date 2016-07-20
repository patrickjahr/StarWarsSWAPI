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
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IDataService _dataService;
        private readonly ILocalStorageService _localStorageService;
        private readonly ObservableCollection<People> _currentPersons = new ObservableCollection<People>(); 

        public PeopleRepository(IDataService dataService, ILocalStorageService localStorageService)
        {
            _dataService = dataService;
            _localStorageService = localStorageService;
        }

        public async Task<ObservableCollection<People>> GetAllPeopleAsync(bool refresh = false)
        {
            if (refresh)
            {
                _currentPersons.Clear();
                await LoadDataAndSaveFileAsync();
            }
            else if (!_currentPersons.HasItems())
            {
                var file = await _localStorageService.ReadStringFromStorageAsync("people");
                if (file.IsNeitherNullNorEmpty())
                {
                    var people = JsonConvert.DeserializeObject<ObservableCollection<People>>(file);
                    _currentPersons.AddRange(people);
                }
                else
                {
                    await LoadDataAndSaveFileAsync();
                }
            }
            return _currentPersons;
        }

        public async Task<ObservableCollection<People>> SearchPeopleAsync(string searchText)
        {
            if (!_currentPersons.HasItems())
                await LoadDataAndSaveFileAsync();

            return _currentPersons.HasItems() ? _currentPersons.Where(item => item.name.ContainsIgnoreCase(searchText)).ToObservableCollection() : new ObservableCollection<People>();
        }

        private async Task LoadDataAndSaveFileAsync()
        {
            var result = await _dataService.GetAllPeople();
            _currentPersons.AddRange(result.results);
            while (!String.IsNullOrEmpty(result.nextPageNo))
            {
                result = await _dataService.GetAllPeople(result.nextPageNo);
                _currentPersons.AddRange(result.results);
            }

            var resultString = JsonConvert.SerializeObject(_currentPersons);
            await _localStorageService.WriteStringToStorageAsync("people", resultString);
        }

        public async Task<People> GetPeopleAsync(string id)
        {
            var currentPeople = new People();
            if (_currentPersons.HasItems())
            {
                var person = _currentPersons.FirstOrDefault(item => item.url == id);
                if (person != null)
                    return person;
            }
            var result = await _dataService.GetPeople(id);
            if (result != null)
                currentPeople = result;

            return currentPeople;
        }
    }
}