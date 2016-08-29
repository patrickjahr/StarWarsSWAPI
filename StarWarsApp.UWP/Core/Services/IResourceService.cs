namespace Core.Services
{
    public interface IResourceService
    {
        /// <summary>
        /// Gets the string for the given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetString(string key);

        /// <summary>
        /// Changes the primary language.
        /// </summary>
        /// <param name="newLanguage">The new language identifier e.g. "de" for german or "en" for english.</param>
        void ChangePrimaryLanguage(string newLanguage);
    }
}