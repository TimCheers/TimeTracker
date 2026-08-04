namespace TimeTracker.Domain.Entities;

public class TasksInGroup
{
    public int Id { get; set; }

    public int TaskId { get; set; }
    public TaskItem Task { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;
}