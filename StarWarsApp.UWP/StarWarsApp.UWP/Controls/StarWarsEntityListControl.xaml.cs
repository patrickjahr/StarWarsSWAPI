using System;
using System.Collections;
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
using Core.Utils;
using Core.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Model;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace StarWarsApp.UWP.Controls
{
    public sealed partial class StarWarsEntityListControl : UserControl
    {
        private readonly OverviewViewModel _vm;


        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            "ItemSource", typeof (IEnumerable<SharpEntity>), typeof (StarWarsEntityListControl), new PropertyMetadata(default(IEnumerable<SharpEntity>)));

        public IEnumerable<SharpEntity> ItemSource
        {
            get { return (IEnumerable<SharpEntity>) GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(
            "SearchText", typeof (string), typeof (StarWarsEntityListControl), new PropertyMetadata(default(string)));

        public string SearchText
        {
            get { return (string) GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register(
            "DisplayMemberPath", typeof (string), typeof (StarWarsEntityListControl), new PropertyMetadata(default(string)));

        public string DisplayMemberPath
        {
            get { return (string) GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedEntityProperty = DependencyProperty.Register(
            "SelectedEntity", typeof (SharpEntity), typeof (StarWarsEntityListControl), new PropertyMetadata(default(SharpEntity)));

        public SharpEntity SelectedEntity
        {
            get { return (SharpEntity) GetValue(SelectedEntityProperty); }
            set { SetValue(SelectedEntityProperty, value); }
        }

        public static readonly DependencyProperty PlaceHolderTextProperty = DependencyProperty.Register(
            "PlaceHolderText", typeof (string), typeof (StarWarsEntityListControl), new PropertyMetadata(default(string)));

        public string PlaceHolderText
        {
            get { return (string) GetValue(PlaceHolderTextProperty); }
            set { SetValue(PlaceHolderTextProperty, value); }
        }

        public static readonly DependencyProperty DisplaySearchBoxProperty = DependencyProperty.Register(
            "DisplaySearchBox", typeof (bool), typeof (StarWarsEntityListControl), new PropertyMetadata(default(bool)));

        public bool DisplaySearchBox
        {
            get { return (bool) GetValue(DisplaySearchBoxProperty); }
            set { SetValue(DisplaySearchBoxProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof (string), typeof (StarWarsEntityListControl), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public StarWarsEntityListControl()
        {
            this.InitializeComponent();
            _vm = (OverviewViewModel)DataContext;
        }
        
        private async void Search_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            await _vm.SearchCommand.ExecuteAsync();
        }

        private async void StarWarEntity_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var currentItem = e.ClickedItem as SharpEntity;
            if (currentItem == null) return;
            var navService = SimpleIoc.Default.GetInstance<Core.Services.INavigationService>();
            if (currentItem is People)
            {
                await SimpleIoc.Default.GetInstance<PersonViewModel>().InitAsyncCommand.ExecuteAsync(currentItem);
                navService.Navigate(NavigationTarget.PERSON_DETAIL_PAGE);
            }

            else if (currentItem is Film)
            {
                await SimpleIoc.Default.GetInstance<MovieViewModel>().InitAsyncCommand.ExecuteAsync(currentItem);
                navService.Navigate(NavigationTarget.MOVIE_DETAIL_PAGE);
            }

            else if (currentItem is Planet)
            {
                await SimpleIoc.Default.GetInstance<PlanetViewModel>().InitAsyncCommand.ExecuteAsync(currentItem);
                navService.Navigate(NavigationTarget.PLANET_DETAIL_PAGE);
            }

            else if (currentItem is Starship)
            {
                await SimpleIoc.Default.GetInstance<StarshipViewModel>().InitAsyncCommand.ExecuteAsync(currentItem);
                navService.Navigate(NavigationTarget.STARSHIP_DETAIL_PAGE);
            }


            else if (currentItem is Vehicle)
            {
                var vm = SimpleIoc.Default.GetInstance<VehicleViewModel>();
                await vm.InitAsyncCommand.ExecuteAsync(currentItem);

                navService.Navigate(NavigationTarget.VEHICLE_DETAIL_PAGE);
            }

            else if (currentItem is Specie)
            {
                await SimpleIoc.Default.GetInstance<SpecieViewModel>().InitAsyncCommand.ExecuteAsync(currentItem);
                navService.Navigate(NavigationTarget.SPECIE_DETAIL_PAGE);
            }
        }
    }
}
