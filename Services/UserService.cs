using System;
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

        public async Task<User> LoginUser(string Username, string Password)
        {
            User user = userContext.DbSet().Where(s => s.Username == Username).Include(e => e.Role).FirstOrDefault();

            return user ?? throw new Exception("The username or password is incorrect");
        }
    }
}