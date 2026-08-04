using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Data.Configurations;

public class SessionLogConfiguration  : IEntityTypeConfiguration<SessionLog>
{
    public void Configure(EntityTypeBuilder<SessionLog> builder)
    {
        builder.Property(p => p.EventType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Note)
            .HasMaxLength(1000);
    }
}