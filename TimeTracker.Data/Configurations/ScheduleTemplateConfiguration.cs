using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class ScheduleTemplateConfiguration : IEntityTypeConfiguration<ScheduleTemplate>
{
    public void Configure(EntityTypeBuilder<ScheduleTemplate> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(t => t.TasksInSchedules)
            .WithOne(ts => ts.Schedule)
            .HasForeignKey(ts => ts.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(t => t.Sessions)
            .WithOne(ts => ts.Schedule)
            .HasForeignKey(ts => ts.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}