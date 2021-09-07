using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMP332.Models;

namespace CMP332.Data
{
    public class SQLRepository<T> : IRepository<T> where T : ModelBase
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public DbSet<T> DbSet()
        {
            // Not sure if this is bad pracise but its used to do finds without having to type expressions
            return context.Set<T>();
        }

        public async Task<bool> Commit()
        {
            await context.SaveChangesAsync();
            return true;
        }

        public void Delete(int Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public T Find(int Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public async Task<int> UpdateAsync<T>(T item) where T : ModelBase
        {
            var entity = dbSet.Find(item.Id);
            if (entity == null)
            {
                return 0;
            }

            return context.SaveChanges();
        }
    }
}