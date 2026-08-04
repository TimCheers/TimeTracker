namespace TimeTracker.Domain.Entities;


public class SessionLog
{
    public int Id { get; set; }

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public DateTime Timestamp { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? Note { get; set; } 
}