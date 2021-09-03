using System.Threading.Tasks;
using System.Windows.Input;
using CMP332.Commands;
using CMP332.Models;
using CMP332.Services;
using CMP332.Stores;

namespace CMP332.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {

        private string _currentPassword;
        private string _newPassword;
        private string _errorMessage;

        public ICommand UpdatePasswordCommand { get;}
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
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

        public AccountViewModel(UserStore userStore, CloseModalNavigationService closeModalNavigationService)
        {
            UpdatePasswordCommand = new AsyncRelayCommand(UpdatePassword,(ex)=>ErrorMessage = ex.Message);

        }

        private async Task UpdatePassword()
        {
            Task.Delay(200);
        }
    }
}