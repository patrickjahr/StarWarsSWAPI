using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StarWarsApp.UWP.Converter
{
    /// <summary>
    /// SelectedValue converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(Visibility))
            {
                if (parameter is string && ((string)parameter).Equals("reverse", StringComparison.CurrentCultureIgnoreCase))
                    return !(value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
                else
                    return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;    
            }
            if (targetType == typeof(double) || targetType == typeof(int))
            {
                if (parameter is string && ((string)parameter).Equals("reverse", StringComparison.CurrentCultureIgnoreCase))
                    return !(value is bool && (bool)value) ? 1 : 0;
                else
                    return (value is bool && (bool)value) ? 1 : 0;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
