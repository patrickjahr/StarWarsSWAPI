using System.Threading.Tasks;
using Model;

namespace Core.Services
{
    public interface IDataService
    {
        /// <summary>
        /// get a specific resource by url
        /// </summary>
        Task<T> GetSingleByUrl<T>(string url) where T : SharpEntity;

        /// <summary>
        /// get a specific people resource
        /// </summary>
        Task<People> GetPeople(string id);

        /// <summary>
        /// get all the people resources
        /// </summary>
        Task<SharpEntityResults<People>> GetAllPeople(string pageNumber = "1");

        /// <summary>
        /// get a specific film resource
        /// </summary>
        Task<Film> GetFilm(string id);

        /// <summary>
        /// get all the film resources
        /// </summary>
        Task<SharpEntityResults<Film>> GetAllFilms(string pageNumber = "1");

        /// <summary>
        /// get a specific planet resource
        /// </summary>
        Task<Planet> GetPlanet(string id);

        /// <summary>
        /// get all the planet resources
        /// </summary>
        Task<SharpEntityResults<Planet>> GetAllPlanets(string pageNumber = "1");

        /// <summary>
        /// get a specific specie resource
        /// </summary>
        Task<Specie> GetSpecie(string id);

        /// <summary>
        /// get all the specie resources
        /// </summary>
        Task<SharpEntityResults<Specie>> GetAllSpecies(string pageNumber = "1");

        /// <summary>
        /// get a specific starship resource
        /// </summary>
        Task<Starship> GetStarship(string id);

        /// <summary>
        /// get all the starship resources
        /// </summary>
        Task<SharpEntityResults<Starship>> GetAllStarships(string pageNumber = "1");

        /// <summary>
        /// get a specific vehicle resource
        /// </summary>
        Task<Vehicle> GetVehicle(string id);

        /// <summary>
        /// get all the vehicle resources
        /// </summary>
        Task<SharpEntityResults<Vehicle>> GetAllVehicles(string pageNumber = "1");
    }
}