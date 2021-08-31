using System.Data.Entity;
using CMP332.Models;

namespace CMP332.Data
{
    public class DataContext :DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}