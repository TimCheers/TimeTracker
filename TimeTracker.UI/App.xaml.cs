using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.Data;
using TimeTracker.Domain.Entities;
using TimeTracker.UI.ViewModels;

namespace TimeTracker.UI;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());
        
        services.AddTransient<MainWindowViewModel>();
        
        services.AddTransient<Func<ScheduleTemplate, ScheduleEditorViewModel>>(sp =>
            schedule => new ScheduleEditorViewModel(sp.GetRequiredService<AppDbContext>(), schedule));

        Services = services.BuildServiceProvider();
    }
}