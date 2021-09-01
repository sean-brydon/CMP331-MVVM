using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    class Property
    {
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public User MaintanceStaff { get; set; }
        public User LettingAgent { get; set; }
        public Lettor CurrentLettor { get; set; }
        public virtual ICollection<Inspection> Inspections { get; set; }
        public virtual ICollection<Job> MaintanceJobs { get; set; }

    }
}
