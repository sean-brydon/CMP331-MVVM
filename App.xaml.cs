
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CMP332.Services;
using CMP332.Stores;
using CMP332.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CMP332
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    //public partial class App : Application
    //{
    //    private readonly NavigationStore _navigationStore;
    //    private readonly ModalNavigationStore _modalNavigationStore;
    //    private readonly UserStore _userStore;

    //    public App()
    //    {
    //        _navigationStore = new NavigationStore();
    //        _modalNavigationStore = new ModalNavigationStore();
    //        //_userStore = new UserStore();
    //    }

    //    protected override void OnStartup(StartupEventArgs e)
    //    {
    //        // Create the navigation service for login
    //        INavigationService navigationService = CreateLoginNavigationService();
    //        // Navigate to the defined few model
    //        navigationService.Navigate();

    //        MainWindow = new MainWindow()
    //        {
    //            DataContext = new MainViewModel(_navigationStore, _modalNavigationStore)
    //        };
    //        MainWindow.Show();

    //        base.OnStartup(e);
    //    }

    //    private INavigationService CreateLoginNavigationService()
    //    {
    //        // Creates the navigation service for viewmodel and adds it to the store singletone
    //        // Then Creates the Viewmodel and attaches it to the store. 
    //        // Allowing us to access only one instance of a particular view at a time.
    //        return new NavigationService<LoginViewModel>(_navigationStore, CreateLoginViewModel);
    //    }

    //    private LoginViewModel CreateLoginViewModel()
    //    {
    //        //return new LoginViewModel(CreateHomeNavigationService());
    //        return new LoginViewModel();
    //    }

    //    private INavigationService CreateHomeNavigationService()
    //    {
    //        return new NavigationService<HomeViewModel>(_navigationStore, CreateHomeViewModel);
    //    }

    //    private HomeViewModel CreateHomeViewModel()
    //    {
    //        return new HomeViewModel(CreateLoginNavigationService());
    //    }
    //}

    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            // Singletons for stores
            services.AddSingleton<UserStore>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();
            
            // Main Navigation Service
            // The view model defined in here also changes the application start point
            services.AddSingleton<INavigationService>(CreateHomeNavigationSystem);
            services.AddSingleton<CloseModalNavigationService>();

            // Create a home view model and add it the dep array
            services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateLoginNavigationService(s)));
            
            // Create the login view model and require its dependencies 
            services.AddTransient<LoginViewModel>(CreateLoginViewModel);

            // Setup the required dependencies for the nav bar
            services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);

            // Add the main view model as a depedancy - the requirements for this will be populated later on
            services.AddSingleton<MainViewModel>();


            // Set the data context of the main window to all the requirements of main window
            // This is pretty strange as there is techincally no data context of mainwindow
            // However in the onStartup all the requirements get bubbled into the Mainwindow from the initalNavigationSystem
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            // Build all of the services into the new DI container in .NET 5
            _serviceProvider = services.BuildServiceProvider();
        }

      

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initalNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initalNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationSystem(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),serviceProvider.GetRequiredService<HomeViewModel>,serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }


        private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<LoginViewModel>(serviceProvider.GetRequiredService<ModalNavigationStore>(), () => serviceProvider.GetRequiredService<LoginViewModel>());
        }


        private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
        {
            // We use composite here because we need to be able to close the modal inside the modal view but we also need access to the home page on login
            CompositeNavigationService navigationService = new CompositeNavigationService(serviceProvider.GetRequiredService<CloseModalNavigationService>());

            return new LoginViewModel(serviceProvider.GetRequiredService<UserStore>(),navigationService);
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<UserStore>(),CreateHomeNavigationSystem(serviceProvider),CreateLoginNavigationService(serviceProvider));
        }


    }
}
