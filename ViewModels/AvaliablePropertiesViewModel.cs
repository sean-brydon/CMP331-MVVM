using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CMP332.Commands;
using CMP332.Models;
using CMP332.Services;

namespace CMP332.ViewModels
{
    public class AvaliablePropertiesViewModel:ViewModelBase
    {
        private List<Property> _properties;

        public List<Property> FilteredProperties
        {
            get { return _properties; }
            set
            {
                _properties = value; 
                OnPropertyChanged(nameof(FilteredProperties));
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

        private int _minRooms;

        public int MinRoomsInput
        {
            get { return _minRooms; }
            set
            {
                _minRooms = value;
                OnPropertyChanged(nameof(MinRoomsInput));
            }
        }

        private string _maxPrice;

        public string MaxRoomPrice
        {
            get { return _maxPrice; }
            set
            {
                _maxPrice = value;
                OnPropertyChanged(nameof(MaxRoomPrice));
            }
        }


        public ICommand FilterAvailableCommand { get; }
        public ICommand GetAllCommand { get; }
        public ICommand FilterMinRoomsCommand { get; }
        public ICommand FilterMaxPriceCommand { get; }

        public AvaliablePropertiesViewModel()
        {
            FilteredProperties = new PropertyService().GetAllWithIncludes();  
            FilterAvailableCommand = new AsyncRelayCommand(FilterAvailable,(ex)=>ErrorMessage = ex.Message);
            FilterMinRoomsCommand = new AsyncRelayCommand(FilterMinRooms, (ex)=>ErrorMessage = ex.Message);
            FilterMaxPriceCommand = new AsyncRelayCommand(FilterMaxPrice, (ex)=>ErrorMessage = ex.Message);
            GetAllCommand = new AsyncRelayCommand(GetAll,(ex)=>ErrorMessage = ex.Message);
        }

        public async Task FilterAvailable()
        {
            FilteredProperties = new PropertyService().GetAllPropertiesWithNoLettor();
        }

        public async Task GetAll()
        {
            FilteredProperties = new PropertyService().GetAll();
        }

        public async Task FilterMinRooms()
        {
            FilteredProperties = await new PropertyService().GetAllWhereMinRooms(MinRoomsInput);
        }

        public async Task FilterMaxPrice()
        {
            FilteredProperties = await new PropertyService().GetAllWhereMaxPrice(float.Parse(MaxRoomPrice));
        }
    }
}