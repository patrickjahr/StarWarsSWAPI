using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;

namespace Core.Repositories
{
    public interface IStarshipRepository
    {
        Task<ObservableCollection<Starship>> GetAllStarshipsAsync(bool refresh = false);
        Task<ObservableCollection<Starship>> SearchStarships(string searchText);

        Task<Starship> GetStarshipAsync(string id);
    }
}