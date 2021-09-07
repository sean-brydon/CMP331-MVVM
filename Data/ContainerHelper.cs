using CMP332.Models;
using Unity;
using Unity.Lifetime;

namespace CMP332.Data
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;

        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRepository<User>, SQLRepository<User>>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IRepository<Role>, SQLRepository<Role>>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IRepository<Property>, SQLRepository<Property>>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IRepository<Lettor>, SQLRepository<Lettor>>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IRepository<Job>, SQLRepository<Job>>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IRepository<Inspection>, SQLRepository<Inspection>>(
               new ContainerControlledLifetimeManager());

            _container.RegisterType<IRepository<Invoice>, SQLRepository<Invoice>>(
              new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}