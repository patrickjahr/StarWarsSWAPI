using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;

namespace Core.Repositories
{
    public interface IVehiclesRepository
    {
        Task<ObservableCollection<Vehicle>> GetAllVehiclesAsync(bool refresh = false);
        Task<ObservableCollection<Vehicle>> SearchVehicles(string searchText);

        Task<Vehicle> GetVehicleAsync(string id);
    }
}