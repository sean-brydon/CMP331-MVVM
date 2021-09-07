using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMP332.Models;

namespace CMP332.Data
{
    public interface IRepository<T> where T : ModelBase
    {
        IQueryable<T> Collection();
        DbSet<T> DbSet();
        Task<bool> Commit();
        void Delete(int Id);
        T Find(int Id);
        void Insert(T t);
        void Update(T t);
        Task<int> UpdateAsync<T>(T item) where T : ModelBase;
    }
}