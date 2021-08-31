using System;
using System.DirectoryServices.ActiveDirectory;

namespace CMP332.Models
{
    public class User : ModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }


        public User()
        {
            
        }

        public User(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        // Used in seeding to ensure we dont get new data every time we run a migration
        public User(int id, string username, string password, Role role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
        }



    }
}