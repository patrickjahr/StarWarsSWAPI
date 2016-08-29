using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Core.Services;
using Core.Utils.Commands;
using GalaSoft.MvvmLight;
using Model;

namespace Core.ViewModel
{
    public abstract class MainViewModel : ViewModelBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; RaisePropertyChanged(); }
        }
        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
        }
    }
}