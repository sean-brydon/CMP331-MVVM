
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
    public class LoginViewModel : ViewModelBase
    {

        private string _username;
        private string _password;
        private string _errorMessage;
        private UserStore _userStore;
        private CloseModalNavigationService _loginModalService;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand{ get; }

        public LoginViewModel(UserStore userStore,CloseModalNavigationService loginNavigationService)
        {
            _userStore = userStore;
            _loginModalService = loginNavigationService;
            LoginCommand = new AsyncRelayCommand(Login,(ex)=>ErrorMessage = ex.Message);
        }

        private async Task Login()
        {
            User user =  new UserService().LoginUser(Username, Password);
            //Task.Delay(2000);
            _userStore.LoggedInUser = user;

            _loginModalService.Navigate();

        }
    }
}
