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
        public ICommand NavigateAccountCommand { get; set; }


        public bool IsLoggedIn => _userStore.IsLoggedIn;
        public bool IsAdmin => _userStore.LoggedInUser.Role.Name == "System Admin";

        public NavigationBarViewModel(UserStore userStore,INavigationService homeNavigationService, INavigationService loginNavigationService, INavigationService accountNavigationService)
        {
            _userStore = userStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigateAccountCommand = new NavigateCommand(accountNavigationService);
            LogoutUserCommand = new LogoutCommand(userStore);


            _userStore.LoggedInUserChanged += OnCurrentUserChanged;
        }

        private void OnCurrentUserChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsAdmin));
        }

        public override void Dispose()
        {
            _userStore.LoggedInUserChanged -= OnCurrentUserChanged;

            base.Dispose();
        }
    }
}