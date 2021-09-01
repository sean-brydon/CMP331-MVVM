using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    public class Inspection : ModelBase
    {
        public DateTime LastInspectionDate { get; set; }
        public DateTime NextInspectionDate { get; set; }

        public string InspectionType{ get; set; }
    }

    // Use this as a string enum InspectionType.QuarterlyInspections etc...
    public class InspectionType
    {
        public const string QuarterlyInspections = "Quarterly Inspections";
        public const string YearlyGasInspections = "Yearly Gas Inspections";
        public const string FiveYearElectricity = "Five Year Electricity Inspection";
        public const string ElectricityInspectionBefore = "Electricity Inspection Before New Tentant";
    }
}
