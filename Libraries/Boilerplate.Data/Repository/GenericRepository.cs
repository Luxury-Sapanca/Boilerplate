namespace Boilerplate.Data.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, ISoftDelete
{
    protected DataContext Context;

    public GenericRepository(DataContext context)
    {
        Context = context;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().SingleOrDefaultAsync(expression);
    }

    public async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await SaveChangesAsync();

        return entity;
    }

    public async Task SoftDeleteAsync(T entity)
    {
        T existingEntity = await Context.Set<T>().FindAsync(entity.Id);

        if (existingEntity != null)
        {
            existingEntity.IsDeleted = true;

            await SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);

        await SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        T existingEntity = await Context.Set<T>().FindAsync(entity.Id);

        if (existingEntity != null)
        {
            Context.Entry(existingEntity).State = EntityState.Modified;
            Context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await SaveChangesAsync();
        }

        return existingEntity!;
    }

    private async Task SaveChangesAsync()
    {
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Context.ChangeTracker.Clear();

            throw;
        }
    }
}