using GalaSoft.MvvmLight.Ioc;

namespace Core.ViewModel
{
    public class ViewModelManager
    {
        public OverviewViewModel OverviewViewModel => SimpleIoc.Default.GetInstance<OverviewViewModel>();
        public PersonViewModel PersonViewModel => SimpleIoc.Default.GetInstance<PersonViewModel>();
        public MovieViewModel MovieViewModel => SimpleIoc.Default.GetInstance<MovieViewModel>();
        public SpecieViewModel SpecieViewModel => SimpleIoc.Default.GetInstance<SpecieViewModel>();
        public StarshipViewModel StarshipViewModel => SimpleIoc.Default.GetInstance<StarshipViewModel>();
        public VehicleViewModel VehicleViewModel => SimpleIoc.Default.GetInstance<VehicleViewModel>();
        public PlanetViewModel PlanetViewModel => SimpleIoc.Default.GetInstance<PlanetViewModel>();
    }
}