using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using StarWarsApp.UWP.ViewModel;
using Template10.Utils;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StarWarsApp.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private readonly SettingsViewModel _vm;
        public SettingsPage()
        {
            this.InitializeComponent();
            _vm = SimpleIoc.Default.GetInstance<SettingsViewModel>();
            DataContext = _vm;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = _vm.SettingsPartViewModel.UseLightThemeButton
                ? ElementTheme.Light
                : ElementTheme.Dark;
        }
    }
}
