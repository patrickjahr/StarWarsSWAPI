using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Core.Utils.Extentions;
using static System.Double;

namespace StarWarsApp.UWP.Converter
{
    public class DoubleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double currentValue;
            if (!TryParse(value.ToString(), out currentValue)) return Visibility.Collapsed;
            double currentParameter;
            if (!string.IsNullOrEmpty(parameter.ToString()) && TryParse(parameter.ToString(), out currentParameter))
            {
                return Math.Abs(currentParameter - currentValue) <= 0 ? Visibility.Visible : Visibility.Collapsed;

            }
            return currentValue > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}