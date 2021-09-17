using System.Collections.Generic;
using System.Linq;
using CMP332.Data;
using CMP332.Models;
using Unity;

namespace CMP332.Services
{
    public class LettorService
    {
        private IRepository<Lettor> _lettorRepository;

        public LettorService()
        {
            _lettorRepository = ContainerHelper.Container.Resolve<IRepository<Lettor>>();
        }

        public List<Lettor> GetAllLettors()
        {
            return _lettorRepository.Collection().ToList();
        }
    }
}