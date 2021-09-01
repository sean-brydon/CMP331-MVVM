using System;
using System.Data.Entity.Migrations;
using CMP332.Data;
using CMP332.Models;

namespace CMP332.Migrations
{
    public class Configuration:DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            
        }

        protected override void Seed(DataContext context)
        {

            Console.WriteLine("**************************************************************");
            Console.WriteLine("** WARNING: Remember, you should only run Seed method once! **");
            Console.WriteLine("**************************************************************");

            // TODAYS Date
            DateTime dateTime = DateTime.Now;

            // Add Users
            User SysAdmin = new User(1, "SysAdmin", "Password99!", new Role("System Admin"));
            User LettingAgent = new User(2, "LettingAgent", "Password99!", new Role("Letting Agent"));
            User Maintance = new User(3, "MaintenanceStaff", "Password99!", new Role("Maintenance Staff"));


            context.Users.AddOrUpdate(SysAdmin);
            context.Users.AddOrUpdate(LettingAgent);
            context.Users.AddOrUpdate(Maintance);

            // Create Maintance Jobs
            Job job1 = new Job(1, "Broken Heating", "Gas won't turn on for the heating", false);
            Job job2 = new Job(2, "Door won't close", "Door won't close all the way and it's letting in a draft", false);
            Job job3 = new Job(3, "Dripping Tap", "Bathroom tap is dripping", false);
            Job job4 = new Job(4, "Light not working", "Garage Light doesnt work", false);
            Job job5 = new Job(5, "Light not working", "Bathroom Light doesnt work", true);


            context.Jobs.AddOrUpdate(job1);
            context.Jobs.AddOrUpdate(job2);
            context.Jobs.AddOrUpdate(job3);
            context.Jobs.AddOrUpdate(job4);
            context.Jobs.AddOrUpdate(job5);

            // Create Inspections

            Inspection YearlyGasInspection = new Inspection(1,dateTime.AddMonths(11),InspectionType.YearlyGasInspections,false);
            Inspection YearlyGasInspection1 = new Inspection(2,dateTime.AddMonths(1).AddDays(4),InspectionType.YearlyGasInspections,false);
            Inspection FiveYearlyElectric = new Inspection(3,dateTime.AddMonths(1),InspectionType.FiveYearElectricity,false);
            Inspection FiveYearlyElectric2 = new Inspection(6,dateTime.AddMonths(-2),InspectionType.FiveYearElectricity,true);
            Inspection QuarterlyInspections = new Inspection(4,dateTime.AddDays(11),InspectionType.QuarterlyInspections,false);
            Inspection QuarterlyInspection2 = new Inspection(5,dateTime.AddDays(-5),InspectionType.QuarterlyInspections,true);

            context.Inspections.AddOrUpdate(YearlyGasInspection);
            context.Inspections.AddOrUpdate(YearlyGasInspection1);
            context.Inspections.AddOrUpdate(FiveYearlyElectric);
            context.Inspections.AddOrUpdate(FiveYearlyElectric2);
            context.Inspections.AddOrUpdate(QuarterlyInspections);
            context.Inspections.AddOrUpdate(QuarterlyInspection2);

            // Create Lettors

            Lettor lettor1 = new Lettor(1, "Dan Sharp", dateTime.AddYears(-1), dateTime.AddMonths(1));
            // No longer in contract
            Lettor lettor2 = new Lettor(2, "Sam Sharp", dateTime.AddYears(-4), dateTime.AddMonths(-1));
            Lettor lettor3 = new Lettor(3, "Sam Smith", dateTime.AddYears(-2), dateTime.AddMonths(5));
            Lettor lettor4 = new Lettor(4, "Adam John", dateTime.AddYears(-5), dateTime.AddDays(2));
            Lettor lettor5 = new Lettor(5, "Chloe Smart", dateTime.AddYears(-1), dateTime.AddMonths(1));

            context.Lettors.AddOrUpdate(lettor1);
            context.Lettors.AddOrUpdate(lettor2);
            context.Lettors.AddOrUpdate(lettor3);
            context.Lettors.AddOrUpdate(lettor4);
            context.Lettors.AddOrUpdate(lettor5);

            // Properties 

            base.Seed(context);
        }
    }
}