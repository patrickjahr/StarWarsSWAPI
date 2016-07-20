using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Core.Utils.Extentions;
using Model;

namespace StarWarsApp.UWP.Converter
{
    public class RandomVehiclePictureConverter : IValueConverter
    {
        private static readonly List<KeyValuePair<string, BitmapImage>> _vehicleImages = new List<KeyValuePair<string,BitmapImage>>
        {
            //Vehicle
            new KeyValuePair<string,BitmapImage>("Sand Crawler",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/sandcrawler.png"))),
            new KeyValuePair<string,BitmapImage>("AT-AT",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/AT-AT-icon.png"))),
            new KeyValuePair<string,BitmapImage>("AT-RT",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/AT-RT-icon.png"))),
            new KeyValuePair<string,BitmapImage>("AT-ST",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/AT-ST-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("T-16 skyhopper",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/B-Wing-icon.png"))),
            new KeyValuePair<string,BitmapImage>("X-34 landspeeder",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Landspeeder-icon.png"))),
            new KeyValuePair<string,BitmapImage>("TIE/LN starfighter",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Tie-Fighter-01-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Snowspeeder",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Snowspeeder.png"))),
            new KeyValuePair<string,BitmapImage>("TIE bomber",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Tie-Bomber-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Sail barge",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Bantha-II cargo skiff",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/bantha_cargo_skiff.png"))),
            new KeyValuePair<string,BitmapImage>("TIE/IN interceptor",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Tie-Interceptor-icon"))),
            new KeyValuePair<string,BitmapImage>("Imperial Speeder Bike",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Speeder-Bike-icon"))),
            new KeyValuePair<string,BitmapImage>("Vulture Droid",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Vulture-Droid-icon"))),
            //new KeyValuePair<string,BitmapImage>("Multi-Troop Transport",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Armored Assault Tank",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/AAT-Battle-Tank-icon"))),
            new KeyValuePair<string,BitmapImage>("C-9979 landing craft",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Trade-Federation-Landing-Ship-icon"))),
            //new KeyValuePair<string,BitmapImage>("Tribubble bongo",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Sith speeder",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/FC-20_Speeder.png"))),
            new KeyValuePair<string,BitmapImage>("Zephyr-G swoop bike",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Speeder-Bike-icon.png"))),
            new KeyValuePair<string,BitmapImage>("XJ-6 airspeeder",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("LAAT/i",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("LAAT/c",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Emergency Firespeeder",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Droid tri-fighter",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Oevvaor jet catamaran",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Clone turbo tank",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Corporate Alliance tank",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Droid gunship",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("AT-TE",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("SPHA",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Flitknot speeder",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("Neimoidian shuttle",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Geonosian",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Geonosian_NEGAS.png"))),

            //Starships
            new KeyValuePair<string,BitmapImage>("Sentinel-class landing craft",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Imperial-Shuttle-01-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Death Star",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Death-Star-2nd-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Millenium Falcon",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Millenium-Falcon-01-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Y-Wing",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Y-Wing-icon.png"))),
            new KeyValuePair<string,BitmapImage>("X-Wing",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/X-Wing-02-icon.png"))),
            new KeyValuePair<string,BitmapImage>("TIE Advanced x1",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Tie-Advanced-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Executor",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Lusankya-CE5.png"))),
            new KeyValuePair<string,BitmapImage>("Slave 1",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Slave-I-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Imperial Shuttle",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Imperial-Shuttle-02-icon.png"))),
            new KeyValuePair<string,BitmapImage>("Calamari Cruiser",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Mon-Calamari-Star-Cruiser-icon.png"))),
            new KeyValuePair<string,BitmapImage>("A-wing",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/A-Wing-icon.png"))),
            //new KeyValuePair<string,BitmapImage>("EF76 Nebulon-B escort",new BitmapImage(new Uri("ms-appx:///Assets/vehicles/Tie-Advanced-icon.png"))),
        };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var random = new Random();
            var vehicle = value as Vehicle;
            if (vehicle != null)
            {
                var image = _vehicleImages.FirstOrDefault(item => item.Key.ContainsIgnoreCase(vehicle.name));
                return image.Value ?? _vehicleImages[random.Next(0, _vehicleImages.Count - 1)].Value;
            }
            return  _vehicleImages[random.Next(0, _vehicleImages.Count - 1)].Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}