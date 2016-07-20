using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Core.Message;
using Core.Utils;
using Core.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StarWarsApp.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplashScreen : Page
    {
        public ExtendedSplashScreen()
        {
            this.InitializeComponent();
            Messenger.Default.Register<AppInitCompletedMessage>(this, OnAppInitCompleted);
        }
        
        private void OnAppInitCompleted(AppInitCompletedMessage obj)
        {
            Messenger.Default.Unregister(this);
            var navService = SimpleIoc.Default.GetInstance<Core.Services.INavigationService>();
            navService.Navigate(NavigationTarget.OVERVIEW_PAGE);
        }

        private void ExtendedSplashScreen_OnLoaded(object sender, RoutedEventArgs e)
        {
            StbStartLoad.Begin();
        }
    }
}
