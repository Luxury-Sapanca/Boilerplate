using Boilerplate.Domain.Entities;

namespace Boilerplate.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dummy>();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Dummy> Dummies { get; set; }
}