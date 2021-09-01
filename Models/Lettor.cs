using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    public class Lettor : ModelBase
    {
        public string FullName { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

        public Lettor()
        {

        }

        // Used in seeding only
        public Lettor(int id, string name, DateTime contractStartDate, DateTime contractEndDate)
        {
            Id = id;
            FullName = name;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;
        }
    }
}
