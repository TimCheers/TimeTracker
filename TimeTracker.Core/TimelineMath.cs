namespace TimeTracker.Core;

public static class TimelineMath
{
    public static double MinutesToPixels(TimeOnly sessionStart, TimeOnly sessionEnd, double canvasWidth)
    {
        var totalMinutes = (sessionEnd.ToTimeSpan() - sessionStart.ToTimeSpan()).TotalMinutes;
        return canvasWidth / totalMinutes;
    }

    public static double CalculateLeft(TimeOnly sessionStart, TimeOnly taskStart, double pixelsPerMinute)
    {
        var offsetMinutes = (taskStart.ToTimeSpan() - sessionStart.ToTimeSpan()).TotalMinutes;
        return offsetMinutes * pixelsPerMinute;
    }

    public static double CalculateWidth(TimeOnly taskStart, TimeOnly taskEnd, double pixelsPerMinute)
    {
        var durationMinutes = (taskEnd.ToTimeSpan() - taskStart.ToTimeSpan()).TotalMinutes;
        return durationMinutes * pixelsPerMinute;
    }
}