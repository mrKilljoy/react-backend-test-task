using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using react_backend_test_task_data.Models;

namespace react_backend_test_task_data.Configurations;

public class TreeEntityConfiguration : BaseModelEntityConfiguration<Tree>
{
    public override void Configure(EntityTypeBuilder<Tree> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany<TreeNode>(x => x.Nodes)
            .WithOne()
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.TreeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}