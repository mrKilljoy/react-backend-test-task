using Microsoft.EntityFrameworkCore;
using react_backend_test_task_data.Configurations;
using react_backend_test_task_data.Models;

namespace react_backend_test_task_data;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    
    public virtual DbSet<TreeNode> TreeNodes { get; set; }
    
    public virtual DbSet<Tree> Trees { get; set; }
    
    public virtual DbSet<JournalEntry> JournalEntries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TreeNodeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new JournalEntryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TreeEntityConfiguration());
    }
}