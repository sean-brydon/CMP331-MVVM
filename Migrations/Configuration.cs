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

            
            context.Users.AddOrUpdate(new User(1, "Test","Test",new Role("Test")));


            base.Seed(context);
        }
    }
}