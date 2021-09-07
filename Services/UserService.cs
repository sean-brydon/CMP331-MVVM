using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMP332.Data;
using CMP332.Models;
using Unity;

namespace CMP332.Services
{
    public class UserService
    {

        private IRepository<User> userContext;

        public UserService()
        {
            userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
        }

        public User LoginUser(string Username, string Password)
        {
            // Get user from DB querying on the username and password
            // In a real world application we would compare this password against the hash in the database.
            User user = userContext.DbSet().Where(s => s.Username == Username && s.Password == Password).Include(e => e.Role).FirstOrDefault();
            //User user = new User("test", "test", new Role("test1234"));
            return user ?? throw new Exception("The username or password is incorrect");
        }


        public void UpdatePassword(int id, string currentPassword, string newPassword)
        {
            User user = userContext.DbSet().FirstOrDefault(s => s.Id == id);

            if (user == null) throw new Exception("This user does not exist ");
            if (user.Password != currentPassword) throw new Exception("This is not your current password");

            // Update found user 
            user.Password = newPassword;

            userContext.Update(user);

        }

        public List<User> GetAllUsers()
        {
            List<User> users = userContext.Collection().Include("Role").ToList();

            return users;
        }
    }
}