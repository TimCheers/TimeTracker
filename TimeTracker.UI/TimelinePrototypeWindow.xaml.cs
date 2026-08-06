using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TimeTracker.Core;

namespace TimeTracker.UI;

public partial class TimelinePrototypeWindow : Window
{
    public TimelinePrototypeWindow()
    {
        InitializeComponent();

        var dayStart = new TimeOnly(9, 0);
        var dayEnd = new TimeOnly(20, 30);
        var canvasWidth = 1200.0;

        var pixelsPerMinute = TimelineMath.MinutesToPixels(dayStart, dayEnd, canvasWidth);

        AddTaskBlock(new TimeOnly(9, 0), new TimeOnly(10, 0), "First task", dayStart, pixelsPerMinute);
        AddTaskBlock(new TimeOnly(11, 0), new TimeOnly(12, 0), "Task 2", dayStart, pixelsPerMinute);
    }

    private void AddTaskBlock(TimeOnly start, TimeOnly end, string title, TimeOnly dayStart, double pixelsPerMinute)
    {
        var left = TimelineMath.CalculateLeft(dayStart, start, pixelsPerMinute);
        var width = TimelineMath.CalculateWidth(start, end, pixelsPerMinute);

        var rect = new Rectangle
        {
            Width = width,
            Height = 60,
            Fill = Brushes.Orange
        };

        var label = new TextBlock { Text = title };

        Canvas.SetLeft(rect, left);
        Canvas.SetTop(rect, 10);
        Canvas.SetLeft(label, left + 5);
        Canvas.SetTop(label, 15);

        MyCanvas.Children.Add(rect);
        MyCanvas.Children.Add(label);
    }
}