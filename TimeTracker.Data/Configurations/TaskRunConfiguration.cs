using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class TaskRunConfiguration  : IEntityTypeConfiguration<TaskRun>
{
    public void Configure(EntityTypeBuilder<TaskRun> builder)
    {
        
    }
}