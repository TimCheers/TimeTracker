namespace TimeTracker.Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    public ICollection<TasksInGroup> TaskInGroups { get; set; } = new List<TasksInGroup>();
}