using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Services;

public class BaseService<T> : IBaseService<T> where T: BaseEntity
{
    private readonly ApplicationDbContext _context;

    public BaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();
    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry =  Table.Remove(model);
        return entityEntry.State == EntityState.Deleted; 
    }

    public async Task<bool> RemoveAsync(string id)
    {
        T model = Table.FirstOrDefault(x => x.Id == int.Parse(id));
        return Remove(model);
    }
    public bool Update(T model)
    {
        EntityEntry<T> entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    
    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(x=>x.Id==int.Parse(id));
    }
}