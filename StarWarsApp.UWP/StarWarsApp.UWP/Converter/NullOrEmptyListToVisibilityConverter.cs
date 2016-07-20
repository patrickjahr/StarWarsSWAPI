using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StarWarsApp.UWP.Converter
{
    public class NullOrEmptyListToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IList && ((IList)value).Count > 0)
            {
                return !String.IsNullOrEmpty(parameter?.ToString()) ? Visibility.Collapsed : Visibility.Visible;
            }

            return !String.IsNullOrEmpty(parameter?.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
