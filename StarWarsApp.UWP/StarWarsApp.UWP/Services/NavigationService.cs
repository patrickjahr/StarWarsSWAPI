using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Core.Services;
using Core.Utils;
using GalaSoft.MvvmLight.Command;
using StarWarsApp.UWP.Views;

namespace StarWarsApp.UWP.Services
{
    public class NavigationService : INavigationService
    {
        private static Frame _rootFrame;
        private static volatile bool _initialized = false;

        public bool CanGoBack { get { return _rootFrame != null && _rootFrame.CanGoBack; } }

        public ICommand GoBackCommand { get; private set; }

        public NavigationService()
        {
            GoBackCommand = new RelayCommand(GoBack);
            SystemNavigationManager.GetForCurrentView().BackRequested += this.OnBackRequested;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            GoBack();
        }

        private readonly Dictionary<string, Type> _availablePages = new Dictionary<string, Type>
        {
            { NavigationTarget.OVERVIEW_PAGE, typeof(MainPage) },
            { NavigationTarget.MOVIE_DETAIL_PAGE, typeof(MovieDetailPage) },
            { NavigationTarget.PERSON_DETAIL_PAGE, typeof(PersonDetailPage) },
            { NavigationTarget.PLANET_DETAIL_PAGE, typeof(PlanetDetailPage) },
            { NavigationTarget.SPECIE_DETAIL_PAGE, typeof(SpecieDetailPage) },
            { NavigationTarget.STARSHIP_DETAIL_PAGE, typeof(StarshipDetailPage) },
            { NavigationTarget.VEHICLE_DETAIL_PAGE, typeof(VehicleDetailPage) },
            { NavigationTarget.SETTINGS_PAGE, typeof(SettingsPage) }
        };


        public void Initialize(object rootFrame)
        {
            _initialized = true;
            _rootFrame = rootFrame as Frame;
        }

        public void Navigate(string targetPage)
        {
            try
            {
                if (!_initialized)
                {
                    Debug.WriteLine("NavigationService.cs | Navigate | NOT INITIALIZED YET");
                    return;
                }
                if (_rootFrame == null)
                {
                    _rootFrame = new Frame();
                    if (!(Window.Current.Content is Frame))
                    {
                        Window.Current.Content = _rootFrame;
                    }
                }

                if (_rootFrame?.SourcePageType?.Name == targetPage)
                    return; //don't navigate to the actual page again

                var targetPageType = _availablePages[targetPage];

                if (_rootFrame.CurrentSourcePageType != targetPageType)
                    _rootFrame.Navigate(targetPageType);
            }
            catch (Exception e)
            {
                Debug.WriteLine("NavigationService.cs | Navigate | " + e);
                throw;
            }
        }

        public void Navigate(string targetPage, bool removeBackEntry = false, bool clearBackStack = false)
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            try
            {
                if (_rootFrame == null)
                {
                    Debug.WriteLine("NavigationService.cs | GoBack | Please initialize rootframe");
                    return;
                }

                if (_rootFrame.CanGoBack)
                    _rootFrame.GoBack();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NavigationService.cs | GoBack | " + e);
                throw;
            }
        }

        public void GoBackToPage(Uri page)
        {
            throw new NotImplementedException();
        }

        public void GoBackToPage(string page)
        {
            throw new NotImplementedException();
        }

        public void ClearBackStack()
        {
            if (_rootFrame != null)
            {
                if (_rootFrame.BackStackDepth > 0)
                {
                    _rootFrame.SetNavigationState("1,0");
                }
            }
        }

        public void RemoveBackEntry()
        {
            throw new NotImplementedException();
        }

        public string GetActualPageName()
        {
            return _rootFrame.SourcePageType.Name;
        }

        public Uri GetCurrentUri()
        {
            throw new NotImplementedException();
        }

        public Uri GetLastPageUri()
        {
            throw new NotImplementedException();
        }

        public void GoToLoginPage(bool removeBackEntry = false, bool clearBackStack = false)
        {
            throw new NotImplementedException();
        }

        public void GoToExpertsPage(bool removeBackEntry = false, bool clearBackStack = false)
        {
            throw new NotImplementedException();
        }

        public void GoToMainPage(bool removeBackEntry = false, bool clearBackStack = false, bool checkinAtLocation = false)
        {
            ClearBackStack();
            Navigate(NavigationTarget.OVERVIEW_PAGE);
        }

        public void GoToMainPageWithoutChangePassword()
        {
            throw new NotImplementedException();
        }
    }
}