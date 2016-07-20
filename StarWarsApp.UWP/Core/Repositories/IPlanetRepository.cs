using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;

namespace Core.Repositories
{
    public interface IPlanetRepository
    {
        Task<ObservableCollection<Planet>> GetAllPlanetsAsync(bool refresh = false);
        Task<ObservableCollection<Planet>> SearchPlanets(string searchText);

        Task<Planet> GetPlanetAsync(string id);
    }
}