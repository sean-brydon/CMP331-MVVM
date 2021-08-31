using CMP332.ViewModels;

namespace CMP332.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel { set; }
    }
}