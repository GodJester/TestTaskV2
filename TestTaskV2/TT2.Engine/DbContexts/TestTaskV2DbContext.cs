using Microsoft.EntityFrameworkCore;
using TT2.Engine.Entities;

namespace TT2.Engine.DbContexts;

public class TestTaskV2DbContext : DbContext
{
    public TestTaskV2DbContext(DbContextOptions<TestTaskV2DbContext> options) : base(options)
    {
    }
    
    
    public DbSet<Event> Events { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TestTaskV2");
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(TestTaskV2DbContext).Assembly);
    }
}