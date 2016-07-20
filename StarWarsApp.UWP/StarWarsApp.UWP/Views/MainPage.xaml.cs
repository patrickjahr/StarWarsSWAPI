using System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Core.Services;
using Core.Utils;
using Core.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StarWarsApp.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly OverviewViewModel _vm;
        public MainPage()
        {
            this.InitializeComponent();
            _vm = (OverviewViewModel)DataContext;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =  AppViewBackButtonVisibility.Collapsed;
        }
        
        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.SearchText = String.Empty;
            _vm.ResetSearchCommand.Execute();
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<INavigationService>().Navigate(NavigationTarget.SETTINGS_PAGE);
        }
    }
}
