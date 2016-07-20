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
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly IDataService _dataService;
        private readonly ILocalStorageService _localStorageService;
        private readonly ObservableCollection<Vehicle> _currentVehicles = new ObservableCollection<Vehicle>();

        public VehiclesRepository(IDataService dataService, ILocalStorageService localStorageService)
        {
            _dataService = dataService;
            _localStorageService = localStorageService;
        }

        public async Task<ObservableCollection<Vehicle>> GetAllVehiclesAsync(bool refresh = false)
        {
            if (refresh)
            {
                _currentVehicles.Clear();
                await LoadDataAndSaveFileAsync();
            }
            else if (!_currentVehicles.HasItems())
            {
                var file = await _localStorageService.ReadStringFromStorageAsync("vehicles");
                if (file.IsNeitherNullNorEmpty())
                {
                    var vehicles = JsonConvert.DeserializeObject<ObservableCollection<Vehicle>>(file);
                    _currentVehicles.AddRange(vehicles);
                }
                else
                {
                    await LoadDataAndSaveFileAsync();
                }
            }
            return _currentVehicles;
        }

        public async Task<ObservableCollection<Vehicle>> SearchVehicles(string searchText)
        {
            if (!_currentVehicles.HasItems())
                await LoadDataAndSaveFileAsync();

            return _currentVehicles.HasItems() ? _currentVehicles.Where(item => item.name.ContainsIgnoreCase(searchText)).ToObservableCollection() : new ObservableCollection<Vehicle>();
        }

        private async Task LoadDataAndSaveFileAsync()
        {
            var result = await _dataService.GetAllVehicles();
            _currentVehicles.AddRange(result.results);
            while (!String.IsNullOrEmpty(result.nextPageNo))
            {
                result = await _dataService.GetAllVehicles(result.nextPageNo);
                _currentVehicles.AddRange(result.results);
            }

            var resultString = JsonConvert.SerializeObject(_currentVehicles);
            await _localStorageService.WriteStringToStorageAsync("vehicles", resultString);
        }

        public async Task<Vehicle> GetVehicleAsync(string id)
        {
            var currentVehicle = new Vehicle();
            if (_currentVehicles.HasItems())
            {
                var vehicle = _currentVehicles.FirstOrDefault(item => item.url == id);
                if (vehicle != null)
                    return vehicle;
            }
            var result = await _dataService.GetVehicle(id);
            if (result != null)
                currentVehicle = result;

            return currentVehicle;
        }
    }
}