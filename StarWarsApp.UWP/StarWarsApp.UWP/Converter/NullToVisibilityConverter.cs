using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StarWarsApp.UWP.Converter
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(Visibility))
            {
                if (parameter == null)
                    return value == null ? Visibility.Visible : Visibility.Collapsed;

                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }

            if (targetType == typeof(double))
            {
                if (parameter == null)
                    return value == null ? 1 : 0;

                return value == null ? 0 : 1;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
