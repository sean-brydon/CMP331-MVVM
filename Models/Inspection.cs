using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    public class Inspection : ModelBase
    {
        public DateTime InspectionDate { get; set; }

        public string InspectionType{ get; set; }

        public bool InspectionCompleted { get; set; }
        public Inspection()
        {

        }


        // Only Used in seeding
        public Inspection(int id, DateTime inspectionDate,string inspectionType,bool inspectionCompleted)
        {
            Id = id;
            InspectionDate = inspectionDate;
            InspectionType = inspectionType;
            InspectionCompleted = inspectionCompleted;
        }
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
