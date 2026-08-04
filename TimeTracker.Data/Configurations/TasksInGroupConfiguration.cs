using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class TasksInGroupConfiguration : IEntityTypeConfiguration<TasksInGroup>
{
    public void Configure(EntityTypeBuilder<TasksInGroup> builder)
    {
        
    }
}