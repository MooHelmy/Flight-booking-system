using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GenericRepo<T>(ApplicationDbContext context) : IGeneric<T> where T : class
{
    public async Task<IEnumerable<T?>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // EF.Property بيسمحلنا نوصل لخاصية "Id" من غير ما T يكون مقيّد بـ interface معين
        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task<int> CreateAsync(T entity)
    {
        context.Set<T>().Add(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);

        if (entity is null)
        {
            return 0;
        }

        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync();
    }
}
