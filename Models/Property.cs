using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    public class Property : ModelBase
    {
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public User MaintanceStaff { get; set; }
        public User LettingAgent { get; set; }
        public Lettor CurrentLettor { get; set; }
        public List<Inspection> Inspections { get; set; }
        public List<Job> MaintanceJobs { get; set; }

        public Property()
        {

        }


        // This is only used in seeding
        public Property(int id, string address, int numOfRooms,User maintanceStaff,User lettingAgent,Lettor currentLettor, List<Inspection> inspections, List<Job> maintanceJobs)
        {
            Id = id;
            Address = address;
            NumberOfRooms = numOfRooms;
            MaintanceStaff = maintanceStaff;
            LettingAgent = lettingAgent;
            CurrentLettor = currentLettor;
            Inspections = inspections;
            MaintanceJobs = maintanceJobs;
        }

    }
}
