using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(8);

        builder.HasMany(t => t.TasksInGroups)
            .WithOne(ts => ts.Group)
            .HasForeignKey(ts => ts.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}