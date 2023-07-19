using Microsoft.EntityFrameworkCore.Metadata.Builders;
using react_backend_test_task_data.Models;

namespace react_backend_test_task_data.Configurations;

public class JournalEntryEntityConfiguration : BaseModelEntityConfiguration<JournalEntry>
{
    public override void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Data)
            .IsRequired();
    }
}