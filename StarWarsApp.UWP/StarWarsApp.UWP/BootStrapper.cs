using System;
using System.Collections.Generic;
using Core.Repositories;
using Core.Services;
using Core.Utils;
using Core.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using StarWarsApp.UWP.Services;
using StarWarsApp.UWP.ViewModel;
using StarWarsApp.UWP.Views;

namespace StarWarsApp.UWP
{
    public class BootStrapper
    {
        public BootStrapper()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            //SimpleIoc.Default.Register<INavigationService>(CreateNavigationService);
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IPeopleRepository, PeopleRepository>();
            SimpleIoc.Default.Register<IFilmRepository, FilmRepository>();
            SimpleIoc.Default.Register<IStarshipRepository, StarshipRepository>();
            SimpleIoc.Default.Register<IVehiclesRepository, VehiclesRepository>();
            SimpleIoc.Default.Register<ISpeciesRepository, SpeciesRepository>();
            SimpleIoc.Default.Register<IPlanetRepository, PlanetRepository>();
            SimpleIoc.Default.Register<ILocalStorageService, LocalStorageService>();
            SimpleIoc.Default.Register<OverviewViewModel>();
            SimpleIoc.Default.Register<PersonViewModel>();
            SimpleIoc.Default.Register<MovieViewModel>();
            SimpleIoc.Default.Register<StarshipViewModel>();
            SimpleIoc.Default.Register<VehicleViewModel>();
            SimpleIoc.Default.Register<PlanetViewModel>();
            SimpleIoc.Default.Register<SpecieViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        //private INavigationService CreateNavigationService()
        //{
        //    var navigationService = new GalaSoft.MvvmLight.Views.NavigationService();
        //    navigationService.Configure(NavigationTarget.OVERVIEW_PAGE, typeof(MainPage));
        //    navigationService.Configure(NavigationTarget.MOVIE_DETAIL_PAGE, typeof(MovieDetailPage));
        //    navigationService.Configure(NavigationTarget.PERSON_DETAIL_PAGE, typeof(PersonDetailPage));
        //    navigationService.Configure(NavigationTarget.PLANET_DETAIL_PAGE, typeof(PlanetDetailPage));
        //    navigationService.Configure(NavigationTarget.STARSHIP_DETAIL_PAGE, typeof(StarshipDetailPage));
        //    navigationService.Configure(NavigationTarget.VEHICLE_DETAIL_PAGE, typeof(VehicleDetailPage));
        //    navigationService.Configure(NavigationTarget.SPECIE_DETAIL_PAGE, typeof(SpecieDetailPage));
        //    navigationService.Configure(NavigationTarget.SETTINGS_PAGE, typeof(SettingsPage));
        //    return navigationService;
        //}
    }
}