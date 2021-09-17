using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CMP332.Commands;
using CMP332.Models;
using CMP332.Services;

namespace CMP332.ViewModels
{
    class CreatePropertyViewModel : ViewModelBase
    {
        private readonly INavigationService _closeModalsService;


        #region Vars

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        private string _editPropertyAddress;

        public string EditPropertyAddress
        {
            get => _editPropertyAddress;
            set
            {
                _editPropertyAddress = value;
                OnPropertyChanged(nameof(EditPropertyAddress));
            }
        }


        private string _editNumberOfRooms;

        public string EditNumberOfRooms
        {
            get => _editNumberOfRooms;
            set
            {
                _editNumberOfRooms = value;
                OnPropertyChanged(nameof(EditNumberOfRooms));
            }
        }

        private List<User> _allMaintanceStaff;

        public List<User> AllMaintanceStaff
        {
            get => _allMaintanceStaff;
            set
            {
                _allMaintanceStaff = value;
                OnPropertyChanged(nameof(AllMaintanceStaff));
            }
        }

        private List<User> _allLettingAgents;

        public List<User> AllLettingAgents
        {
            get => _allLettingAgents;
            set
            {
                _allLettingAgents = value;
                OnPropertyChanged(nameof(AllLettingAgents));
            }
        }

        private List<Inspection> _allInspections;

        public List<Inspection> AllInspections
        {
            get => _allInspections;
            set
            {
                _allInspections = value;
                OnPropertyChanged(nameof(AllInspections));
            }
        }

        private List<Job> _allJobs;

        public List<Job> AllJobs
        {
            get => _allJobs;
            set
            {
                _allJobs = value;
                OnPropertyChanged(nameof(AllJobs));
            }
        }

        private Inspection _editInspection;

        public Inspection EditInspection
        {
            get { return _editInspection; }
            set
            {
                _editInspection = value;
                OnPropertyChanged(nameof(EditInspection));
            }
        }

        private User _editMaintanceStaff;

        public User EditMaintanceStaff
        {
            get { return _editMaintanceStaff; }
            set
            {
                _editMaintanceStaff = value;
                OnPropertyChanged(nameof(EditMaintanceStaff));
            }
        }

        private User _editLettingAgentUser;

        public User EditLettingAgentUser
        {
            get { return _editLettingAgentUser; }
            set
            {
                _editLettingAgentUser = value;
                OnPropertyChanged(nameof(EditLettingAgentUser));
            }
        }

        private Job _editJobs;

        public Job EditJob
        {
            get { return _editJobs; }
            set
            {
                _editJobs = value;
                OnPropertyChanged(nameof(EditJob));
            }
        }

        private List<Lettor> _allLettors;

        public List<Lettor> AllLettors
        {
            get { return _allLettors; }
            set
            {
                _allLettors = value;
                OnPropertyChanged(nameof(AllLettors));
            }
        }

        private Lettor _SelectedLettor;

        public Lettor EditLettor
        {
            get { return _SelectedLettor; }
            set
            {
                _SelectedLettor = value; 
                OnPropertyChanged(nameof(EditLettor));
            }
        }

        private string _costPerMonth;

        public string CostPerMonth
        {
            get { return _costPerMonth; }
            set
            {
                _costPerMonth = value; 
                OnPropertyChanged(nameof(CostPerMonth));
            }
        }


        #endregion
        public ICommand CloseModalCommand { get; }
        public ICommand CreatePropertyCommand { get; }
        public CreatePropertyViewModel(INavigationService closeModalsService)
        {
            _closeModalsService = closeModalsService;
            CreatePropertyCommand = new AsyncRelayCommand(CreateNewProperty, (ex) => ErrorMessage = ex.Message);
            CloseModalCommand = new NavigateCommand(closeModalsService);
            // Populate forms
            AllMaintanceStaff = new UserService().GetAllUsersByRole("Maintenance Staff");
            AllLettingAgents = new UserService().GetAllUsersByRole("Letting Agent");
            AllInspections = new InspectionService().GetIncompletedInspections();
            AllJobs = new JobService().GetAllJobsWhereNoPropertyAssigned();
            AllLettors = new LettorService().GetAllLettors();


        }

        private async Task CreateNewProperty()
        {
            Property newProperty = new Property()
            {
                Inspections = new List<Inspection>(){EditInspection},
                MaintanceJobs = new List<Job>() {EditJob},
                CostPerMonth = float.Parse(CostPerMonth),
                CurrentLettor = EditLettor,
                Address = EditPropertyAddress,
                LettingAgent = EditLettingAgentUser,
                MaintanceStaff = EditMaintanceStaff,
                NumberOfRooms = int.Parse(EditNumberOfRooms)
            };

            if (!await new PropertyService().Create(newProperty))
            {
                throw new Exception("There has been an issue with your request");
            };

            _closeModalsService.Navigate();
        }
    }
}
