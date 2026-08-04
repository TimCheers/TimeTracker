using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class TasksInSessionConfiguration  : IEntityTypeConfiguration<TasksInSession>
{
    public void Configure(EntityTypeBuilder<TasksInSession> builder)
    {
        builder.HasMany(t => t.TaskRuns)
            .WithOne(ts => ts.TasksInSession)
            .HasForeignKey(ts => ts.TaskInSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}