using System.Data.Entity;
using CMP332.Models;

namespace CMP332.Data
{
    public class DataContext :DbContext
    {
        private const string NameOrConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=DatabaseTesting;Integrated Security=True";

        public DataContext() : base(NameOrConnectionString)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}