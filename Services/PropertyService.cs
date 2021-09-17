using CMP332.Data;
using CMP332.Models;
using Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Property> GetAll()
        {
            return _propertyContext.Collection().ToList();
        }


        public List<Property> GetAllWithIncludes()
        {
            return _propertyContext.Collection().Include("MaintanceStaff").Include("LettingAgent").Include("CurrentLettor").Include("Inspections").Include("MaintanceJobs").ToList();
        }

        public List<Property> GetAllPropertiesWithNoLettor()
        {
            return _propertyContext.DbSet().Where(p => p.CurrentLettor == null).ToList();
        }


        public async Task<bool> Delete(int id)
        {
            _propertyContext.Delete(id);
            return await _propertyContext.Commit();
        }

        public async Task Update(Property p)
        {
            _propertyContext.Update(p);
            await _propertyContext.Commit();
        }

        public async Task<bool> Create(Property newProperty)
        {
            _propertyContext.Insert(newProperty);
            return await _propertyContext.Commit();
        }
    }
}
