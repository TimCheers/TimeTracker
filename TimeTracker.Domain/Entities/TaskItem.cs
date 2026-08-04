namespace TimeTracker.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public ICollection<TasksInGroup> TasksInGroups { get; set; } = new List<TasksInGroup>();
    public ICollection<TasksInSchedule> TasksInSchedules { get; set; } = new List<TasksInSchedule>();
    public ICollection<TasksInSession> TasksInSessions { get; set; } = new List<TasksInSession>();
}