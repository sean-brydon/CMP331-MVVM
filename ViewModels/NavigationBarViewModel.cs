using System.Windows.Input;
using CMP332.Commands;
using CMP332.Services;
using CMP332.Stores;

namespace CMP332.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;

        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateLoginCommand { get; set; }
        public ICommand LogoutUserCommand { get; set; }

        public bool IsLoggedIn => _userStore.IsLoggedIn;

        public NavigationBarViewModel(UserStore userStore,INavigationService homeNavigationService, INavigationService loginNavigationService)
        {
            _userStore = userStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            LogoutUserCommand = new LogoutCommand(userStore);


            _userStore.LoggedInUserChanged += OnCurrentUserChanged;
        }

        private void OnCurrentUserChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _userStore.LoggedInUserChanged -= OnCurrentUserChanged;

            base.Dispose();
        }
    }
}