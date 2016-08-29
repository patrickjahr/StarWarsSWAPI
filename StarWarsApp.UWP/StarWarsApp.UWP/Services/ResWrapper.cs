using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Core.Services;

namespace StarWarsApp.UWP.Services
{
    public class ResWrapper : IResourceService
    {
        public static Dictionary<string, string> Strings { get; set; }

        public ResWrapper()
        {
            loadStrings();
        }

        private void loadStrings()
        {
            if (Strings == null || Strings.Count == 0)
            {
                var manager = ResourceManager.Current;
                Strings = toDictionary(manager.MainResourceMap.GetSubtree("Resources"));
            }
        }

        private Dictionary<string, string> toDictionary(ResourceMap resourceMap)
        {
                Dictionary<string, string> strings = new Dictionary<string, string>();
            try
            {
                var context = ResourceContext.GetForViewIndependentUse();
                strings = resourceMap.ToDictionary(p => p.Key, p =>
                {
                    try
                    {
                        return resourceMap.GetValue(p.Key, context).ValueAsString;
                    }
                    catch
                    {
                        Debug.WriteLine("ResWrapper.cs | loadStrings | Resource key missing: {0}", p.Key);
                        throw new Exception(string.Format("Resource key missing: {0}", p.Key));
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine("ResWrapper.cs | toDictionary | " + e);
                throw;
            }
            return strings;
        }

        public string GetString(string key)
        {
            try
            {
                return Strings[key];
            }
            catch
            {
                return string.Format("missing resource: {0}", key);
            }
        }

        public void ChangePrimaryLanguage(string newLanguage)
        {
            try
            {
                ApplicationLanguages.PrimaryLanguageOverride = newLanguage;
                ResourceContext.GetForViewIndependentUse().Reset();
                ResourceContext.GetForCurrentView().Reset();
            }
            catch (Exception e)
            {
                Debug.WriteLine("ResWrapper.cs | ChangePrimaryLanguage | " + e);
            }
        }
    }

}
