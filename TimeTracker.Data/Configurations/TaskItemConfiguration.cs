using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(t => t.TasksInSchedules)
            .WithOne(ts => ts.Task)
            .HasForeignKey(ts => ts.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(t => t.TasksInSessions)
            .WithOne(ts => ts.Task)
            .HasForeignKey(ts => ts.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(t => t.TasksInGroups)
            .WithOne(ts => ts.Task)
            .HasForeignKey(ts => ts.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}