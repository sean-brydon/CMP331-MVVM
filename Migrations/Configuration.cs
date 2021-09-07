using System;
using System.Collections.Generic;
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
            Console.WriteLine("**              WARNING: Seeing the database.               **");
            Console.WriteLine("**************************************************************");

            // TODAYS Date
            DateTime dateTime = DateTime.Now;

            // Roles
            Role sysAdminRole = new Role(1, "System Admin");
            Role lettingAgentRole = new Role(2, "Letting Agent");
            Role maintenanceRole = new Role(3, "Maintenance Staff");
            context.Roles.AddOrUpdate(sysAdminRole);
            context.Roles.AddOrUpdate(lettingAgentRole);
            context.Roles.AddOrUpdate(maintenanceRole);


            // Add Users
            User SysAdmin = new User(1, "SysAdmin", "Password99!",sysAdminRole);
            User LettingAgent = new User(2, "LettingAgent", "Password99!",lettingAgentRole);
            User Maintance = new User(3, "MaintenanceStaff", "Password99!", maintenanceRole);


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

            Inspection YearlyGasInspection = new Inspection(1,InspectionType.YearlyGasInspections,false);
            Inspection YearlyGasInspection1 = new Inspection(2,InspectionType.YearlyGasInspections,false);
            Inspection FiveYearlyElectric = new Inspection(3,InspectionType.FiveYearElectricity,false);
            Inspection FiveYearlyElectric2 = new Inspection(6,InspectionType.FiveYearElectricity,true);
            Inspection QuarterlyInspections = new Inspection(4,InspectionType.QuarterlyInspections,false);
            Inspection QuarterlyInspection2 = new Inspection(5,InspectionType.QuarterlyInspections,true);

            context.Inspections.AddOrUpdate(YearlyGasInspection);
            context.Inspections.AddOrUpdate(YearlyGasInspection1);
            context.Inspections.AddOrUpdate(FiveYearlyElectric);
            context.Inspections.AddOrUpdate(FiveYearlyElectric2);
            context.Inspections.AddOrUpdate(QuarterlyInspections);
            context.Inspections.AddOrUpdate(QuarterlyInspection2);

            // Create Lettors

            Lettor lettor1 = new Lettor (1, "Dan Sharp", dateTime.AddYears(-1), dateTime.AddMonths(1));
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

            List<Inspection> inspections1 = new List<Inspection>();
            inspections1.Add(FiveYearlyElectric);

            List<Job> jobs1 = new List<Job>();
            jobs1.Add(job1);
            jobs1.Add(job3);

            List<Inspection> inspections2 = new List<Inspection>();
            inspections2.Add(YearlyGasInspection1);
            inspections2.Add(QuarterlyInspections);

            List<Job> jobs2 = new List<Job>();
            jobs1.Add(job2);


            List<Inspection> inspections3 = new List<Inspection>();
            inspections2.Add(QuarterlyInspection2);

            List<Job> jobs3 = new List<Job>();
            jobs1.Add(job4);
            jobs1.Add(job5);

            Property property1 = new Property(1, "9 Wallibo Street", 2, Maintance, LettingAgent, lettor1,inspections1,jobs1);
            Property property2 = new Property(2, "16 Wallibo Street", 3, Maintance, LettingAgent, lettor2,inspections2,jobs2);
            Property property3 = new Property(3, "7 Strong Street", 1, Maintance, LettingAgent, lettor3,inspections3,jobs3);
            Property property4 = new Property(4, "7 Strong Street", 5, Maintance, LettingAgent, null, null, null);
            Property property5 = new Property(5, "7 Strong Street", 2, Maintance, LettingAgent, null, null, null);


            context.Properties.AddOrUpdate(property1);
            context.Properties.AddOrUpdate(property2);
            context.Properties.AddOrUpdate(property3);
            context.Properties.AddOrUpdate(property4);
            context.Properties.AddOrUpdate(property5);


            Invoice invoice1 = new Invoice(1, lettor1, 425, dateTime.AddDays(-5));
            Invoice invoice2 = new Invoice(1, lettor3, 405, dateTime.AddDays(1));
            Invoice invoice3 = new Invoice(1, lettor5, 650, dateTime.AddDays(-16));
            Invoice invoice4 = new Invoice(1, lettor4, 1250, dateTime.AddDays(-50));

            context.Invoices.AddOrUpdate(invoice1);
            context.Invoices.AddOrUpdate(invoice2);
            context.Invoices.AddOrUpdate(invoice3);
            context.Invoices.AddOrUpdate(invoice4);

            base.Seed(context);
        }
    }
}