using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMP332.Data;
using CMP332.Models;
using Unity;

namespace CMP332.Services
{
    public class RoleService
    {

        private IRepository<Role> _roleContext;

        public RoleService()
        {
            _roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
        }


        // This service isnt really needed due to the fact there is three fixed roles. 
        // However, its good to future proof the application incase another role gets implemented in the future 
        public List<Role> GetAllRoles()
        {
            List<Role> roles = _roleContext.Collection().ToList();

            return roles;
        }
    }
}