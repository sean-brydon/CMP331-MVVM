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
    public class PropertyViewModel: ViewModelBase
    {
        private  UserStore _userStore;

        #region OnChangeVars

        private List<Property> _properties;

        public List<Property> Properties
        {
            get => _properties;
            set
            {
                _properties = value;
                OnPropertyChanged(nameof(Properties));
            }
        }


        private Property _selectedProperty { get; set; }
        public Property SelectedProperty
        {
            get => _selectedProperty;
            set
            {
                _selectedProperty = value;
                OnPropertyChanged(nameof(SelectedProperty));
                EditNumberOfRooms = SelectedProperty?.NumberOfRooms.ToString();
                EditPropertyAddress = SelectedProperty?.Address;
            }
        }

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

        public Job  EditJob
        {
            get { return _editJobs; }
            set
            {
                _editJobs = value;
                OnPropertyChanged(nameof(EditJob));
            }
        }

        public bool IsAdmin => _userStore.IsAdmin;
        #endregion


        public ICommand NavigateCreatePropertiesModal { get; }
        public ICommand DeleteSelectedPropertyCommand { get; }
        public ICommand UpdatePropertyCommand { get; }

        public PropertyViewModel(UserStore userStore, INavigationService openModalNavigationService)
        {
            _userStore = userStore;
            // Get all properties to populate the table
            Properties = new PropertyService().GetAllWithIncludes();
            // Open Create Properties modal via command TODO - fix
            NavigateCreatePropertiesModal = new NavigateCommand(openModalNavigationService);
            
            // Delete SelectedPropertyCommand
            DeleteSelectedPropertyCommand = new AsyncRelayCommand(DeleteSelectedProperty,(ex)=>ErrorMessage = ex.Message);
            UpdatePropertyCommand = new AsyncRelayCommand(UpdateProperty,(ex)=>ErrorMessage=ex.Message);
            AllMaintanceStaff = new UserService().GetAllUsersByRole("Maintenance Staff");
            AllLettingAgents = new UserService().GetAllUsersByRole("Letting Agent");
            AllInspections = new InspectionService().GetIncompletedInspections();
            AllJobs = new JobService().GetAllJobsWhereNoPropertyAssigned();

        }

        private async Task<bool> DeleteSelectedProperty()
        {
            int SelectedId = SelectedProperty.Id;
            bool sucessDelete = await new PropertyService().Delete(SelectedId);

            if(!sucessDelete) throw new Exception("There was an Error");

            ErrorMessage = "Deleted...";
            return true;
        }

        private async Task UpdateProperty()
        {
            SelectedProperty.Inspections.Add(EditInspection);
            SelectedProperty.MaintanceJobs.Add(EditJob);
            Property updatedProperty = new Property()
            {
                Id = SelectedProperty.Id,
                Address = EditPropertyAddress,
                CostPerMonth =  SelectedProperty.CostPerMonth,
                CurrentLettor =  SelectedProperty.CurrentLettor,
                Inspections = SelectedProperty.Inspections,
                MaintanceStaff =  EditMaintanceStaff,
                LettingAgent = EditLettingAgentUser,
                NumberOfRooms = Int16.Parse(EditNumberOfRooms),
                MaintanceJobs = SelectedProperty.MaintanceJobs
            };

            await new PropertyService().Update(updatedProperty);
        }
    }
}
