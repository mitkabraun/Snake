using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using Snake.ViewModels;
using Snake.Views;

namespace Snake;

public partial class App
{
    public static IHost Host { get; private set; }

    public App()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File("Logs/nearm.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            .CreateLogger();

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog();
                });
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await Host.StartAsync().ConfigureAwait(false);
        MainWindow = Host.Services.GetRequiredService<MainWindow>();
        if (MainWindow != null)
            MainWindow.Visibility = Visibility.Visible;
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await Host.StopAsync().ConfigureAwait(false);
        base.OnExit(e);
    }
}
