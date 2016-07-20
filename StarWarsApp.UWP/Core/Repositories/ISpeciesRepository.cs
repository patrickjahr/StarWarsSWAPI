using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;

namespace Core.Repositories
{
    public interface ISpeciesRepository
    {
        Task<ObservableCollection<Specie>> GetAllSpeciesAsync(bool refresh = false);
        Task<ObservableCollection<Specie>> SearchSpecies(string searchText);

        Task<Specie> GetSpecieAsync(string id);
    }
}