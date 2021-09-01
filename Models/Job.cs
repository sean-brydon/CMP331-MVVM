using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    public class Job: ModelBase
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool JobCompleted { get; set; }

        public Job()
        {

        }

        // This is used for seeding only 
        public Job(int id, string name , string description, bool jobCompleted)
        {
            Id = id;
            Name = name;
            Description = description;
            JobCompleted = jobCompleted; 
        }
    }
}
