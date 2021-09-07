using CMP332.Stores;
using CMP332.Models;
using CMP332.Services;
using CMP332.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using System;

namespace CMP332.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        #region UISetup
        private User _selectedUser;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                // Fire onselected change here to update the UI
                OnPropertyChanged(nameof(HasSelected));
                EditUsername = SelectedUser.Username;
                EditPassword = SelectedUser.Password;
            }
        }

        public bool HasSelected => SelectedUser != null;

        private List<User> _users;

        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private List<Role> _roles;

        public List<Role> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        #endregion
        // Vars to handle the form input and population upon selection
        #region Forms
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

        private string _editUsername;

        public string EditUsername
        {
            get { return _editUsername; }
            set 
            { 
                _editUsername = value;
                OnPropertyChanged(nameof(EditUsername));
            }
        }

        private string _editPassword;

        public string EditPassword
        {
            get { return _editPassword; }
            set
            {
                _editPassword = value;
                OnPropertyChanged(nameof(EditPassword));
            }
        }
        #endregion

        public ICommand UpdateUserCommand { get; private set; }

        public UserViewModel(UserStore userStore)
        {
            _users = new UserService().GetAllUsers();
            _roles = new RoleService().GetAllRoles();
            UpdateUserCommand = new AsyncRelayCommand(UpdateUser, (ex) => ErrorMessage = ex.Message);
        }

        private async Task UpdateUser()
        {
            if(!(EditUsername.Length > 5) && !(EditPassword.Length > 5))
            {
                throw new Exception("Please enter a valid username and password");
            }
            // Create a new user object but force the ID to be the selected user.
            User editedUser = new User()
            {

                Id = SelectedUser.Id,
                Role = SelectedUser.Role,

                // These come from the form 
                Username = EditUsername,
                Password = EditPassword
            };

            await new UserService().UpdateUser(editedUser);

            ErrorMessage = "Account Has been updated please wait...";

        }
    }
}
