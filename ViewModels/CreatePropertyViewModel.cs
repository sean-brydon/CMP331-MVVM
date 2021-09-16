using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CMP332.Commands;
using CMP332.Services;

namespace CMP332.ViewModels
{
    class CreatePropertyViewModel : ViewModelBase
    {

        public ICommand CloseModalCommand;
        public ICommand CreatePropertyCommand;
        public CreatePropertyViewModel(CloseModalNavigationService closeModalsService)
        {
            CloseModalCommand = new NavigateCommand(closeModalsService);
            // Populate forms

        }
    }
}
