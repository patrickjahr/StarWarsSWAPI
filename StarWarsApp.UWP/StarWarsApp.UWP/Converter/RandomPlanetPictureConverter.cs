using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Core.Utils.Extentions;
using Model;

namespace StarWarsApp.UWP.Converter
{
    public class RandomPlanetPictureConverter : IValueConverter
    {
        private static readonly List<KeyValuePair<string, BitmapImage>> _planetImages = new List<KeyValuePair<string,BitmapImage>>
        {
            new KeyValuePair<string,BitmapImage>("Alderaan",new BitmapImage(new Uri("ms-appx:///Assets/planets/blue_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Naboo",new BitmapImage(new Uri("ms-appx:///Assets/planets/blue_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Mustafar",new BitmapImage(new Uri("ms-appx:///Assets/planets/red_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Hoth",new BitmapImage(new Uri("ms-appx:///Assets/planets/sand_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Jakku",new BitmapImage(new Uri("ms-appx:///Assets/planets/sand_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Tatooine",new BitmapImage(new Uri("ms-appx:///Assets/planets/sand_2_planet.png"))),
            new KeyValuePair<string,BitmapImage>("ring_planet",new BitmapImage(new Uri("ms-appx:///Assets/planets/ring_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Dagoba",new BitmapImage(new Uri("ms-appx:///Assets/planets/small_planet.png"))),
            new KeyValuePair<string,BitmapImage>("Endor",new BitmapImage(new Uri("ms-appx:///Assets/planets/small_planet.png"))),
        };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var random = new Random();
            var planet = value as Planet;
            if (planet == null) return _planetImages[random.Next(0, _planetImages.Count - 1)].Value;
            var image = _planetImages.FirstOrDefault(item => item.Key.ContainsIgnoreCase(planet.name));
            return image.Value ?? _planetImages[random.Next(0, _planetImages.Count - 1)].Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}