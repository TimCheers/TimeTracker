using TimeTracker.Data;
using TimeTracker.Domain.Entities;

namespace TimeTracker.UI.ViewModels;

public class ScheduleEditorViewModel
{
    private readonly AppDbContext _context;
    public ScheduleTemplate Schedule { get; }

    public ScheduleEditorViewModel(AppDbContext context, ScheduleTemplate schedule)
    {
        _context = context;
        Schedule = schedule;
    }
}