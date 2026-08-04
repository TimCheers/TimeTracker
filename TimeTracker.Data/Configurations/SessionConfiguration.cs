using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasMany(t => t.SessionLogs)
            .WithOne(ts => ts.Session)
            .HasForeignKey(ts => ts.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(t => t.TasksInSessions)
            .WithOne(ts => ts.Session)
            .HasForeignKey(ts => ts.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}