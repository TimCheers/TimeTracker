using TimeTracker.Domain.Enums;

namespace TimeTracker.Domain.Entities;

public class TasksInSession
{
    public int Id { get; set; }
    
    public int TaskId { get; set; }
    public TaskItem Task { get; set; } = null!;
    
    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;
    
    public int Order { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public TaskRunStatus Status { get; set; }
    
    public ICollection<TaskRun> TaskRuns { get; set; } = new List<TaskRun>();
}