using eComm.Domain.Interfaces;
using eComm.Infrastructure.Data;
using eComm.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace eComm.Infrastructure.Repositories
{
    public class GenericRepostitory<T>(eCommDbcontext dbcontext) : IGeneric<T> where T : class
    {
        public async Task<int> CreateAsync(T entity)
        {
            dbcontext.Set<T>().Add(entity);
            return await dbcontext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await dbcontext.Set<T>().FindAsync(id) ?? throw new ItemNotFoundException($"{id} is not found");
            dbcontext.Set<T>().Remove(entity);
            return await dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbcontext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await dbcontext.Set<T>().FindAsync(id);
            return result!;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            dbcontext.Set<T>().Update(entity);
            return await dbcontext.SaveChangesAsync();
        }
    }
}
