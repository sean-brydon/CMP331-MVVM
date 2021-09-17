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
            // In a real world application we would compare this password a ainst the hash in the database.
            User user = userContext.DbSet().Where(s => s.Username == Username && s.Password == Password).Include(e => e.Role).FirstOrDefault();
            //User user = new User("test", "test", new Role("test1234"));
            return user ?? throw new Exception("The username or password is incorrect");
        }


        public Task<int> UpdatePassword(int id, string currentPassword, string newPassword)
        {
            User user = userContext.DbSet().FirstOrDefault(s => s.Id == id);

            if (user == null) throw new Exception("This user does not exist ");
            if (user.Password != currentPassword) throw new Exception("This is not your current password");

            // Update found user 
            user.Password = newPassword;

            return userContext.UpdateAsync(user);

        }

        public List<User> GetAllUsers()
        {
            List<User> users = userContext.Collection().Include("Role").ToList();

            return users;
        }

        public async Task UpdateUser(User u)
        {
            // Check if username already exists
            User foundUserByUsername = await FindUserByUsername(u);
            if (foundUserByUsername != null && foundUserByUsername.Id != u.Id) throw new Exception("There is already a user with this name");

            userContext.Update(u);
            await userContext.Commit();
        }

        private async Task<User> FindUserByUsername(User u)
        {
            return await userContext.DbSet().FirstOrDefaultAsync(e => e.Username == u.Username);
        }

        public async Task CreateUser(User u)
        {
            User foundUserExists = await FindUserByUsername(u);
            if (foundUserExists != null) throw new Exception("This user already exists");

            userContext.Insert(u);
            await userContext.Commit();
        }

        public async Task DeleteUser(User selectedUser)
        {
            userContext.Delete(selectedUser.Id);
            await userContext.Commit();
        }

        public List<User> GetAllUsersByRole(string rollName)
        {
            return userContext.DbSet().Include("Role").Where(e => e.Role.Name == rollName).ToList();
        }
    }
}