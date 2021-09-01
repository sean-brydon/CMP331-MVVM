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

        public Job()
        {

        }
    }
}
