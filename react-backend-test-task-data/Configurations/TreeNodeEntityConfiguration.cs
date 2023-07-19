using Microsoft.EntityFrameworkCore.Metadata.Builders;
using react_backend_test_task_data.Models;

namespace react_backend_test_task_data.Configurations;

public class TreeNodeEntityConfiguration : BaseModelEntityConfiguration<TreeNode>
{
    public override void Configure(EntityTypeBuilder<TreeNode> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(512)
            .IsRequired();
        
        builder.Property(x => x.TreeId)
            .IsRequired();

        builder.HasIndex(x => new { x.Name, x.TreeId })
            .IsUnique();

        builder.Property(x => x.ParentId);
    }
}