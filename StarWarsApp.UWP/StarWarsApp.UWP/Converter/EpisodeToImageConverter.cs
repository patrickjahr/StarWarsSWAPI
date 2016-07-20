using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Core.Utils.Extentions;

namespace StarWarsApp.UWP.Converter
{
    public class EpisodeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var episode = value.ToString();
            if (episode.IsNeitherNullNorEmpty())
            {
                if (parameter.IsNotNull() && parameter.ToString().IsNeitherNullNorEmpty())
                {
                    switch (episode)
                    {
                        case "1":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode1Logo.jpg"));
                        case "2":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode2Logo.jpg"));
                        case "3":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode3Logo.jpg"));
                        case "4":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode4Logo.jpg"));
                        case "5":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode5Logo.jpg"));
                        case "6":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode6Logo.jpg"));
                        case "7":
                            return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo_wide/Episode7Logo.jpg"));
                    }
                }

                switch (episode)
                {
                    case "1":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode1Logo.jpg"));
                    case "2":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode2Logo.jpg"));
                    case "3":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode3Logo.jpg"));
                    case "4":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode4Logo.jpg"));
                    case "5":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode5Logo.jpg"));
                    case "6":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode6Logo.jpg"));
                    case "7":
                        return new BitmapImage(new Uri("ms-appx:///Assets/movie_logo/Episode7Logo.jpg"));
                }
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}