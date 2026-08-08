using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Domain.Entities;
using TimeTracker.Domain.Enums;
using TimeTracker.UI.Commands;

namespace TimeTracker.UI.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly Func<ScheduleTemplate, ScheduleEditorViewModel> _editorFactory;
    public ICommand AddScheduleCommand { get; }
    public ICommand OpenEditorCommand { get; }
    public ICommand StartSessionCommand { get; }
    public ObservableCollection<ScheduleTemplate> Schedules { get; set; } = new();
    private string _newScheduleTitle = string.Empty;
    public string NewScheduleTitle
    {
        get => _newScheduleTitle;
        set => SetField(ref _newScheduleTitle, value);
    }
    public MainWindowViewModel(AppDbContext context, Func<ScheduleTemplate, ScheduleEditorViewModel> editorFactory)
    {
        _context = context;
        _editorFactory = editorFactory;
        Schedules = new ObservableCollection<ScheduleTemplate>(_context.ScheduleTemplates.ToList());
        AddScheduleCommand = new RelayCommand(AddSchedule);
        OpenEditorCommand = new RelayCommand<ScheduleTemplate>(OpenEditor);
        StartSessionCommand = new RelayCommand<ScheduleTemplate>(StartSession);
    }
    private void AddSchedule()
    {
        ScheduleTemplate newTemplate =  new ScheduleTemplate();
        newTemplate.Title = NewScheduleTitle;
        _context.ScheduleTemplates.Add(newTemplate);
        _context.SaveChanges();
        Schedules.Add(newTemplate);
        NewScheduleTitle = string.Empty;
    }
    private void OpenEditor(ScheduleTemplate? schedule)
    {
        if (schedule is null) return;

        var editorViewModel = _editorFactory(schedule);
        var editorWindow = new ScheduleEditorWindow { DataContext = editorViewModel };
        editorWindow.ShowDialog();
    }

    private void StartSession(ScheduleTemplate? schedule)
    {
        if (schedule is null) return;
        var session = new Session
        {
            Schedule = schedule,
            Title = schedule.Title,
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeOnly.FromDateTime(DateTime.Now),
            EndTime = new TimeOnly(23, 59),
            Status = SessionStatus.Started
        };
        _context.Sessions.Add(session);
        List<TasksInSchedule> tasksInSchedule =
            _context.TasksInSchedules
                .Include(ts => ts.Task)
                .Where(ts => ts.ScheduleId == schedule.Id)
                .ToList();
        List<TasksInSession> tasksInSession = new List<TasksInSession>();
        foreach (var task in tasksInSchedule)
        {
            tasksInSession.Add(new TasksInSession
            {
                Task = task.Task,
                Session = session,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                Order = task.Order,
                Status = TaskRunStatus.Planned
            });
        }
        _context.TasksInSessions.AddRange(tasksInSession);
        _context.SaveChanges();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}