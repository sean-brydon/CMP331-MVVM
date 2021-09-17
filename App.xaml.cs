
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CMP332.Models;
using CMP332.Services;
using CMP332.Stores;
using CMP332.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CMP332
{
   

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
            services.AddTransient<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<UserStore>(),CreateLoginNavigationService(s)));
            
            // Create the login view model and require its dependencies 
            services.AddTransient<LoginViewModel>(CreateLoginViewModel);

            // Naming convention got a little messy here
            services.AddTransient<CreateUserViewModel>(CreateCreateUserViewModel);

            services.AddTransient<AccountViewModel>(CreateAccountViewModel);

            services.AddTransient<UserViewModel>(CreateUserViewModel);
            services.AddTransient<PropertyViewModel>(CreatePropertyViewModel);
            services.AddTransient<CreatePropertyViewModel>(CreateCreatePropertyViewModel);


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

        #region NavigationServices
        private INavigationService CreateHomeNavigationSystem(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),serviceProvider.GetRequiredService<HomeViewModel>,serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private INavigationService CreateUserNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<UserViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<UserViewModel>, serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private INavigationService CreatePropertyNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<PropertyViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<PropertyViewModel>, serviceProvider.GetRequiredService<NavigationBarViewModel>);
        }

        private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<LoginViewModel>(serviceProvider.GetRequiredService<ModalNavigationStore>(), serviceProvider.GetRequiredService<LoginViewModel>);
        }

        private INavigationService CreateAcountNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<AccountViewModel>(serviceProvider.GetRequiredService<ModalNavigationStore>(), serviceProvider.GetRequiredService<AccountViewModel>);
        }

        private INavigationService CreateCreateUserNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<CreateUserViewModel>(serviceProvider.GetRequiredService<ModalNavigationStore>(), serviceProvider.GetRequiredService<CreateUserViewModel>);
        }

        private INavigationService CreateCreateProperCreateCrudNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<CreatePropertyViewModel>(serviceProvider.GetRequiredService<ModalNavigationStore>(), serviceProvider.GetRequiredService<CreatePropertyViewModel>);
        }

        #endregion

        #region ViewModels
        private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
        {
            return new LoginViewModel(serviceProvider.GetRequiredService<UserStore>(),serviceProvider.GetRequiredService<CloseModalNavigationService>());
        }

        private AccountViewModel CreateAccountViewModel(IServiceProvider serviceProvider)
        {
            return new AccountViewModel(serviceProvider.GetRequiredService<UserStore>(), serviceProvider.GetRequiredService<CloseModalNavigationService>());
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<UserStore>(),CreateHomeNavigationSystem(serviceProvider),CreateLoginNavigationService(serviceProvider),CreateAcountNavigationService(serviceProvider),CreateUserNavigationService(serviceProvider),CreatePropertyNavigationService(serviceProvider));
        }

        private UserViewModel CreateUserViewModel(IServiceProvider serviceProvider)
        {
            return new UserViewModel(serviceProvider.GetRequiredService<UserStore>(),CreateCreateUserNavigationService(serviceProvider));
        }
        private CreateUserViewModel CreateCreateUserViewModel(IServiceProvider serviceProvider)
        {
            return new CreateUserViewModel(serviceProvider.GetRequiredService<CloseModalNavigationService>());
        }

        private CreatePropertyViewModel CreateCreatePropertyViewModel(IServiceProvider serviceProvider)
        {
            return new CreatePropertyViewModel(serviceProvider.GetRequiredService<CloseModalNavigationService>());
        }

        private PropertyViewModel CreatePropertyViewModel(IServiceProvider serviceProvider)
        {
            return new PropertyViewModel(serviceProvider.GetRequiredService<UserStore>(), CreateCreateProperCreateCrudNavigationService(serviceProvider));
        }
        #endregion
    }
}
