using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DataService : IDataService
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private string apiUrl = "http://swapi.co/api";
        
        #region Private

        private async Task<string> Request(string url, HttpMethod httpMethod)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = httpMethod.ToString();
            
            HttpWebResponse httpWebResponse = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
            StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Dispose();

            return result;
        }

        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            return parameters.Remove(parameters.Length - 1, 1).ToString();
        }


        

        private async Task<T> GetSingle<T>(string endpoint, Dictionary<string, string> parameters = null) where T : SharpEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            return await GetSingleByUrl<T>(url: $"{apiUrl}{endpoint}{serializedParameters}");
        }

        private async Task<SharpEntityResults<T>> GetMultiple<T>(string endpoint) where T : SharpEntity
        {
            return await GetMultiple<T>(endpoint, null);
        }

        private async  Task<SharpEntityResults<T>> GetMultiple<T>(string endpoint, Dictionary<string, string> parameters) where T : SharpEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            string json = await Request($"{apiUrl}{endpoint}{serializedParameters}", HttpMethod.GET);
            SharpEntityResults<T> swapiResponse = JsonConvert.DeserializeObject<SharpEntityResults<T>>(json);
            return swapiResponse;
        }

        private Dictionary<string, string> GetQueryParameters(string dataWithQuery)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] parts = dataWithQuery.Split('?');
            if (parts.Length > 0)
            {
                string QueryParameter = parts.Length > 1 ? parts[1] : parts[0];
                if (!string.IsNullOrEmpty(QueryParameter))
                {
                    string[] p = QueryParameter.Split('&');
                    foreach (string s in p)
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            string[] temp = s.Split('=');
                            result.Add(temp[0], temp[1]);
                        }
                        else
                        {
                            result.Add(s, string.Empty);
                        }
                    }
                }
            }
            return result;
        }

        private async Task<SharpEntityResults<T>> GetAllPaginated<T>(string entityName, string pageNumber = "1") where T : SharpEntity
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("page", pageNumber);

            SharpEntityResults<T> result = await GetMultiple<T>(entityName, parameters);

            result.nextPageNo = String.IsNullOrEmpty(result.next) ? null : GetQueryParameters(result.next)["page"];
            result.previousPageNo = String.IsNullOrEmpty(result.previous) ? null : GetQueryParameters(result.previous)["page"];

            return result;
        }

        #endregion

        #region Public

        /// <summary>
        /// get a specific resource by url
        /// </summary>
        public async Task<T> GetSingleByUrl<T>(string url) where T : SharpEntity
        {
            string json = await Request(url, HttpMethod.GET);
            T swapiResponse = JsonConvert.DeserializeObject<T>(json);
            return swapiResponse;
        }

        // People
        /// <summary>
        /// get a specific people resource
        /// </summary>
        public async Task<People> GetPeople(string id)
        {
            return await GetSingle<People>("/people/" + id);
        }

        /// <summary>
        /// get all the people resources
        /// </summary>
        public async Task<SharpEntityResults<People>> GetAllPeople(string pageNumber = "1")
        {
            SharpEntityResults<People> result = await GetAllPaginated<People>("/people/", pageNumber);

            return result;
        }

        // Film
        /// <summary>
        /// get a specific film resource
        /// </summary>
        public async Task<Film> GetFilm(string id)
        {
            return await GetSingle<Film>("/films/" + id);
        }

        /// <summary>
        /// get all the film resources
        /// </summary>
        public async Task<SharpEntityResults<Film>> GetAllFilms(string pageNumber = "1")
        {
            SharpEntityResults<Film> result = await GetAllPaginated<Film>("/films/", pageNumber);

            return result;
        }

        // Planet
        /// <summary>
        /// get a specific planet resource
        /// </summary>
        public async Task<Planet> GetPlanet(string id)
        {
            return await GetSingle<Planet>("/planets/" + id);
        }

        /// <summary>
        /// get all the planet resources
        /// </summary>
        public async Task<SharpEntityResults<Planet>> GetAllPlanets(string pageNumber = "1")
        {
            SharpEntityResults<Planet> result = await GetAllPaginated<Planet>("/planets/", pageNumber);

            return result;
        }

        // Specie
        /// <summary>
        /// get a specific specie resource
        /// </summary>
        public async Task<Specie> GetSpecie(string id)
        {
            return await GetSingle<Specie>("/species/" + id);
        }

        /// <summary>
        /// get all the specie resources
        /// </summary>
        public async Task<SharpEntityResults<Specie>> GetAllSpecies(string pageNumber = "1")
        {
            SharpEntityResults<Specie> result = await GetAllPaginated<Specie>("/species/", pageNumber);

            return result;
        }

        // Starship
        /// <summary>
        /// get a specific starship resource
        /// </summary>
        public async Task<Starship> GetStarship(string id)
        {
            return await GetSingle<Starship>("/starships/" + id);
        }

        /// <summary>
        /// get all the starship resources
        /// </summary>
        public async Task<SharpEntityResults<Starship>> GetAllStarships(string pageNumber = "1")
        {
            SharpEntityResults<Starship> result = await GetAllPaginated<Starship>("/starships/", pageNumber);

            return result;
        }

        // Vehicle
        /// <summary>
        /// get a specific vehicle resource
        /// </summary>
        public async Task<Vehicle> GetVehicle(string id)
        {
            return await GetSingle<Vehicle>("/vehicles/" + id);
        }

        /// <summary>
        /// get all the vehicle resources
        /// </summary>
        public async Task<SharpEntityResults<Vehicle>> GetAllVehicles(string pageNumber = "1")
        {
            SharpEntityResults<Vehicle> result = await GetAllPaginated<Vehicle>("/vehicles/", pageNumber);

            return result;
        }

        #endregion
    }
}
