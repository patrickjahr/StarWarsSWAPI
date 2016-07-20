using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Model;

namespace StarWarsApp.UWP.Selector
{
    public class SharpEntityTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate MovieTemplate { get; set; }
        public DataTemplate PeopleDataTemplate { get; set; }
        public DataTemplate VehicleDataTemplate { get; set; }
        public DataTemplate SpecieDataTemplate { get; set; }
        public DataTemplate PlanetDataTemplate { get; set; }


        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if(item is Film)
                return MovieTemplate;
            if (item is People)
                return PeopleDataTemplate;
            if (item is Vehicle)
                return VehicleDataTemplate;
            if (item is Planet)
                return PlanetDataTemplate;
            if (item is Specie)
                return SpecieDataTemplate;

            return DefaultTemplate;
        }
    }
}