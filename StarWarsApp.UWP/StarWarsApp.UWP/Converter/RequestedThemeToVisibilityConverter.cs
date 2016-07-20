using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Core.Utils.Extentions;
using GalaSoft.MvvmLight.Ioc;
using StarWarsApp.UWP.ViewModel;

namespace StarWarsApp.UWP.Converter
{
    public class RequestedThemeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var theme = parameter.ToString();
            if (theme.IsNeitherNullNorEmpty())
            {
                var vm = SimpleIoc.Default.GetInstance<SettingsViewModel>();
                bool isLightTheme = vm.SettingsPartViewModel.UseLightThemeButton;
                switch (theme)
                {
                    case "light":
                        return isLightTheme ? Visibility.Visible : Visibility.Collapsed;
                    case "dark":
                        return isLightTheme ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}