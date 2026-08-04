using TimeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<ScheduleTemplate> ScheduleTemplates => Set<ScheduleTemplate>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<SessionLog> SessionLogs => Set<SessionLog>();
    public DbSet<TaskRun> TaskRuns => Set<TaskRun>();
    public DbSet<TasksInGroup> TasksInGroups => Set<TasksInGroup>();
    public DbSet<TasksInSchedule> TasksInSchedules => Set<TasksInSchedule>();
    public DbSet<TasksInSession> TasksInSessions => Set<TasksInSession>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}