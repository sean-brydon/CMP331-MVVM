using CMP332.Data;
using CMP332.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CMP332.Services
{
    public class InspectionService
    {
        private readonly IRepository<Inspection> _inspectionContext;

        public InspectionService()
        {
            _inspectionContext = ContainerHelper.Container.Resolve<IRepository<Inspection>>();
        }

        public List<Inspection> GetInspectionsFromUser(User u, bool inspectionStatus = false)
        {
            List<Inspection> inspections = _inspectionContext.DbSet().Include("Property").Where(e => (e.property.LettingAgent.Id == u.Id || e.property.MaintanceStaff.Id == u.Id) && e.InspectionCompleted == inspectionStatus).ToList();
            return inspections;
        }
    }
}
