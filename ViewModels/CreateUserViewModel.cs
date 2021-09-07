using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CMP332.Services;
using CMP332.Commands;
using CMP332.Models;

namespace CMP332.ViewModels
{
    class CreateUserViewModel : ViewModelBase
    {

        #region Form Vars
        private string _username;

        public string Username
        {
            get { return _username; }
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        private List<Role> _roles;

        public List<Role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        private Role _selectedRole;

        public Role SelectedRole
        {
            get { return _selectedRole; }
            set { _selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); }
        }

        #endregion


        public ICommand CloseModalCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }
        public CreateUserViewModel(CloseModalNavigationService closeModalService)
        {
            _roles = new RoleService().GetAllRoles();
            CloseModalCommand = new NavigateCommand(closeModalService);
            CreateUserCommand = new AsyncRelayCommand(CreateAccount,ex=>ErrorMessage = ex.Message);
        }

        public async Task CreateAccount()
        {
            User userToCreate = new User(Username, Password, SelectedRole);
            await new UserService().CreateUser(userToCreate);
        }
    }
}
