using System;
using CMP332.Models;

namespace CMP332.Stores
{
    public class UserStore
    {
        private User _loggedInUser;

        public User LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
                LoggedInUserChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => LoggedInUser != null;

        public event Action LoggedInUserChanged;

        public void Logout()
        {
            LoggedInUser = null;
        }
    }
}