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
        public ICommand NavigateAdminCommand { get; set; }
        public ICommand NavigatePropertiesCommand { get; set; }
        public ICommand NavigateReportCommand { get; set; }


        public bool IsLoggedIn => _userStore.IsLoggedIn;
        public bool IsAdmin => _userStore.IsAdmin;
        public bool IsLettingStaffOrAdmin => _userStore.IsAdmin || _userStore.IsLettingAgent;

        public NavigationBarViewModel(UserStore userStore,INavigationService homeNavigationService, 
            INavigationService loginNavigationService, INavigationService accountNavigationService,
            INavigationService userManagementService,INavigationService propertiesNavigationService,
            INavigationService reportNavigationService
            )
        {
            _userStore = userStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigateAccountCommand = new NavigateCommand(accountNavigationService);
            NavigateAdminCommand = new NavigateCommand(userManagementService);
            NavigatePropertiesCommand = new NavigateCommand(propertiesNavigationService);
            NavigateReportCommand = new NavigateCommand(reportNavigationService);
            LogoutUserCommand = new LogoutCommand(userStore);


            _userStore.LoggedInUserChanged += OnCurrentUserChanged;
        }

        private void OnCurrentUserChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsLettingStaffOrAdmin));
        }

        public override void Dispose()
        {
            _userStore.LoggedInUserChanged -= OnCurrentUserChanged;

            base.Dispose();
        }
    }
}