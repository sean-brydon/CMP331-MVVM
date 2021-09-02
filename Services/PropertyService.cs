using CMP332.Data;
using CMP332.Models;
using Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Services
{
    public class PropertyService
    {
        private IRepository<Property> _propertyContext;

        public PropertyService()
        {
            _propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
        }

        public List<Property> GetAllPropertiesByUser(User u)
        {
            return _propertyContext.DbSet().Where(p => p.MaintanceStaff.Id == u.Id || p.LettingAgent.Id == u.Id).ToList();
        }

    }
}
