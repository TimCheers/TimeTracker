using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Domain.Entities;
using TimeTracker.UI.Commands;

namespace TimeTracker.UI.ViewModels;

public class ScheduleEditorViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand AddGroupCommand { get; }
    public ICommand AddTaskToScheduleCommand { get; }
    public ScheduleTemplate Schedule { get; }
    public ObservableCollection<Group> Groups { get; }
    public ObservableCollection<TasksInSchedule> TasksInSchedules { get; }
    private string _newGroupTitle = string.Empty;
    private string _newGroupColor = string.Empty;
    private string _newTaskTitle = string.Empty;
    private Group? _selectedGroup;
    private int _newOrder;
    private string _newStartTime = string.Empty;
    private string _newEndTime = string.Empty;

    public string NewGroupTitle
    {
        get => _newGroupTitle;
        set => SetField(ref _newGroupTitle, value);
    }

    public string NewGroupColor
    {
        get => _newGroupColor;
        set => SetField(ref _newGroupColor, value);
    }

    public string NewTaskTitle
    {
        get => _newTaskTitle;
        set => SetField(ref _newTaskTitle, value);
    }

    public Group? SelectedGroup
    {
        get => _selectedGroup;
        set => SetField(ref _selectedGroup, value);
    }

    public int NewOrder
    {
        get => _newOrder;
        set => SetField(ref _newOrder, value);
    }

    public string NewStartTime
    {
        get => _newStartTime;
        set => SetField(ref _newStartTime, value);
    }

    public string NewEndTime
    {
        get => _newEndTime;
        set => SetField(ref _newEndTime, value);
    }

    public ScheduleEditorViewModel(AppDbContext context, ScheduleTemplate schedule)
    {
        _context = context;
        Schedule = schedule;
        Groups = new ObservableCollection<Group>(_context.Groups.ToList());
        TasksInSchedules = new ObservableCollection<TasksInSchedule>(
            _context.TasksInSchedules
                .Include(ts => ts.Task)
                .Where(ts => ts.ScheduleId == Schedule.Id)
                .ToList()
        );
        AddGroupCommand = new RelayCommand(AddGroup);
        AddTaskToScheduleCommand = new RelayCommand(AddTaskToSchedule);
    }

    private void AddGroup()
    {
        Group newGroup = new Group();
        newGroup.Title = NewGroupTitle;
        newGroup.Color = NewGroupColor;
        _context.Groups.Add(newGroup);
        _context.SaveChanges();
        Groups.Add(newGroup);
        NewGroupTitle = string.Empty;
        NewGroupColor = string.Empty;
    }

    public void AddTaskToSchedule()
    {
        if (!TimeOnly.TryParse(NewStartTime, out var startTime)) return;
        if (!TimeOnly.TryParse(NewEndTime, out var endTime)) return;

        TaskItem newTask = new TaskItem();
        newTask.Title = NewTaskTitle;
        _context.Tasks.Add(newTask);
        TasksInSchedule newTaskInSchedule = new TasksInSchedule();
        newTaskInSchedule.Task = newTask;
        newTaskInSchedule.Schedule = Schedule;
        newTaskInSchedule.Order = NewOrder;
        newTaskInSchedule.StartTime = startTime;
        newTaskInSchedule.EndTime = endTime;
        _context.TasksInSchedules.Add(newTaskInSchedule);
        if (SelectedGroup != null)
        {
            TasksInGroup newTaskInGroup = new TasksInGroup();
            newTaskInGroup.Group = SelectedGroup;
            newTaskInGroup.Task = newTask;
            _context.TasksInGroups.Add(newTaskInGroup);
        }

        _context.SaveChanges();
        TasksInSchedules.Add(newTaskInSchedule);
        NewTaskTitle = string.Empty;
        SelectedGroup = null;
        NewOrder = 0;
        NewStartTime = string.Empty;
        NewEndTime = string.Empty;
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