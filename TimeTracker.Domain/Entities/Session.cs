using TimeTracker.Domain.Enums;

namespace TimeTracker.Domain.Entities;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public SessionStatus Status { get; set; }
    
    public int ScheduleId { get; set; }
    public ScheduleTemplate Schedule { get; set; } = null!;
    
    public ICollection<SessionLog> SessionLogs { get; set; } = new List<SessionLog>();
    public ICollection<TasksInSession> TasksInSessions { get; set; } = new List<TasksInSession>();
}