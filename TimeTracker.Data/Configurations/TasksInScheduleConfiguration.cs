using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class TasksInScheduleConfiguration : IEntityTypeConfiguration<TasksInSchedule>
{
    public void Configure(EntityTypeBuilder<TasksInSchedule> builder)
    {
        
    }
}