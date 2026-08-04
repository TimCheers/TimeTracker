namespace TimeTracker.Domain.Entities;

public class ScheduleTemplate
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public ICollection<TasksInSchedule> TasksInSchedules { get; set; } = new List<TasksInSchedule>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}