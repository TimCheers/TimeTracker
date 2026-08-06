using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TimeTracker.Data;
using TimeTracker.Domain.Entities;
using TimeTracker.UI.Commands;

namespace TimeTracker.UI.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;
    public ObservableCollection<ScheduleTemplate> Schedules { get; set; } = new();
    private string _newScheduleTitle = string.Empty;
    public ICommand AddScheduleCommand { get; }

    public string NewScheduleTitle
    {
        get => _newScheduleTitle;
        set => SetField(ref _newScheduleTitle, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainWindowViewModel(AppDbContext context)
    {
        _context = context;
        Schedules = new ObservableCollection<ScheduleTemplate>(_context.ScheduleTemplates.ToList());
        AddScheduleCommand = new RelayCommand(AddSchedule);
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

    private void AddSchedule()
    {
        ScheduleTemplate newTemplate =  new ScheduleTemplate();
        newTemplate.Title = NewScheduleTitle;
        _context.ScheduleTemplates.Add(newTemplate);
        _context.SaveChanges();
        Schedules.Add(newTemplate);
        NewScheduleTitle = string.Empty;
    }
}