using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMP332.Data;
using CMP332.Models;
using Unity;

namespace CMP332.Services
{
    public class JobService
    {
        private IRepository<Job> _jobContext;

        public JobService()
        {
            _jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
        }

        //public List<Job> GetAllJobsWhereNoPropertyAssigned()
        //{
        //    return _jobContext.DbSet().Where(j => j.Property == null).ToList();
        //}
    }
}
