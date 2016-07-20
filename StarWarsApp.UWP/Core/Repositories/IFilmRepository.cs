using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;

namespace Core.Repositories
{
    public interface IFilmRepository
    {
        Task<ObservableCollection<Film>> GetAllFilms(bool refresh = false);
        Task<ObservableCollection<Film>> SearchFilms(string searchText);
        Task<Film> GetFilmAsync(string id);
    }
}