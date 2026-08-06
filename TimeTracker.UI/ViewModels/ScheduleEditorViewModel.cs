using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TimeTracker.Data;
using TimeTracker.Domain.Entities;
using TimeTracker.UI.Commands;

namespace TimeTracker.UI.ViewModels;

public class ScheduleEditorViewModel: INotifyPropertyChanged
{
    private readonly AppDbContext _context;
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand AddGroupCommand { get; }
    public ScheduleTemplate Schedule { get; }
    public ObservableCollection<Group> Groups { get; }
    private string _newGroupTitle = string.Empty;
    private string _newGroupColor = string.Empty;
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

    public ScheduleEditorViewModel(AppDbContext context, ScheduleTemplate schedule)
    {
        _context = context;
        Schedule = schedule;
        Groups = new ObservableCollection<Group>(_context.Groups.ToList());
        AddGroupCommand = new RelayCommand(AddGroup);
    }
    private void AddGroup()
    {
        Group newGroup =  new Group();
        newGroup.Title = NewGroupTitle;
        newGroup.Color = NewGroupColor;
        _context.Groups.Add(newGroup);
        _context.SaveChanges();
        Groups.Add(newGroup);
        NewGroupTitle = string.Empty;
        NewGroupColor = string.Empty;
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