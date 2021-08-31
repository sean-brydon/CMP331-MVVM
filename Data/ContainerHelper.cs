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
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}