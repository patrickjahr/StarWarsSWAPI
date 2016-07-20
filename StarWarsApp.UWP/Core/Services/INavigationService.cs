using System;
using System.Windows.Input;

namespace Core.Services
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        ICommand GoBackCommand { get; }

        void Initialize(object rootFrame);
        void Navigate(string targetPage);
        void Navigate(string targetPage, bool removeBackEntry = false, bool clearBackStack = false);
        void GoBack();
        void GoBackToPage(Uri page);

        void GoBackToPage(string page);
        void ClearBackStack();

        void RemoveBackEntry();

        string GetActualPageName();
        Uri GetCurrentUri();
        Uri GetLastPageUri();

        void GoToLoginPage(bool removeBackEntry = false, bool clearBackStack = false);
        void GoToExpertsPage(bool removeBackEntry = false, bool clearBackStack = false);
        void GoToMainPage(bool removeBackEntry = false, bool clearBackStack = false, bool checkinAtLocation = false);
    }
}