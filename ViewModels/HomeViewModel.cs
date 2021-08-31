
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


        public HomeViewModel(INavigationService loginNavigationService)
        {
            NavigateToLoginCommand = new NavigateCommand(loginNavigationService);
            // _userStore = userStore;
        }
    }
}
