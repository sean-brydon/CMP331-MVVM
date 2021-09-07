using CMP332.Stores;
using CMP332.Models;
using CMP332.Services;
using System.Collections.Generic;

namespace CMP332.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
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

        // Vars to handle the form input and population upon selection

        public UserViewModel(UserStore userStore)
        {
            _users = new UserService().GetAllUsers();
            _roles = new RoleService().GetAllRoles();
        }
    }
}
