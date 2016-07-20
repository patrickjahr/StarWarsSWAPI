using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;

namespace Core.Repositories
{
    public interface IPeopleRepository
    {
        Task<ObservableCollection<People>> GetAllPeopleAsync(bool refresh = false);
        Task<ObservableCollection<People>> SearchPeopleAsync(string searchText);
        Task<People> GetPeopleAsync(string id);
    }
}