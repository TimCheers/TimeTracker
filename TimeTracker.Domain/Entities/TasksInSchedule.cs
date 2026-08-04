namespace TimeTracker.Domain.Entities;

public class TasksInSchedule
{
    public int Id { get; set; }

    public int TaskId { get; set; }
    public TaskItem Task { get; set; } = null!;

    public int ScheduleId { get; set; }
    public ScheduleTemplate Schedule { get; set; } = null!;

    public int Order { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}