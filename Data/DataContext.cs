using System.Data.Entity;
using CMP332.Models;

namespace CMP332.Data
{
    public class DataContext :DbContext
    {
        private const string NameOrConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=CMP332_DB_FINAL;Integrated Security=True";

        public DataContext() : base(NameOrConnectionString)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Lettor> Lettors { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


    }
}