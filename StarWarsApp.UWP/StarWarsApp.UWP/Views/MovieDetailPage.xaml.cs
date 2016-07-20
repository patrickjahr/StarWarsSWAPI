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
using Core.Utils;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using INavigationService = Core.Services.INavigationService;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StarWarsApp.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MovieDetailPage : Page
    {
        public MovieDetailPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<INavigationService>().Navigate(NavigationTarget.SETTINGS_PAGE);
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<INavigationService>().GoToMainPage(true, true, true);
        }
    }
}
