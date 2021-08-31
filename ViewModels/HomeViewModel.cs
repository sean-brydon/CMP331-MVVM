
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CMP332.Commands;
using CMP332.Models;
using CMP332.Services;
using CMP332.Stores;

namespace CMP332.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateToLoginCommand { get; }
        private UserStore _userStore;

        public bool IsLoggedIn => _userStore.IsLoggedIn;

        private string _username;

        public string Username
        {
            get { return _username; }
            set { 
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _role;

        public string Role
        {
            get { return _role; }
            set { 
                _role = value; 
                OnPropertyChanged(nameof(Role));
            }
        }



        public HomeViewModel(UserStore userStore, INavigationService loginNavigationService)
        {
            NavigateToLoginCommand = new NavigateCommand(loginNavigationService);
            _userStore = userStore;

            _userStore.LoggedInUserChanged += OnCurrentUserChanged;
        }

        private void OnCurrentUserChanged() 
        {
            // rerender the value in this viewmodel when this action fires
            OnPropertyChanged(nameof(IsLoggedIn));
            this.Username = _userStore.LoggedInUser?.Username;
            this.Role = _userStore.LoggedInUser?.Role.Name;
        }

        public override void Dispose()
        {
            // Remove the listener when we navigate away from the page
            _userStore.LoggedInUserChanged -= OnCurrentUserChanged;
            base.Dispose();
        }
    }
}
