using TimeTracker.Domain.Enums;

namespace TimeTracker.Domain.Entities;

public class TaskRun
{
    public int Id { get; set; }

    public int TaskInSessionId { get; set; }
    public TasksInSession TasksInSession { get; set; } = null!;
    
    public DateOnly Date { get; set; }
    public TimeOnly ActualStart { get; set; }
    public TimeOnly ActualEnd { get; set; }

    public TaskRunStatus Status { get; set; }
}