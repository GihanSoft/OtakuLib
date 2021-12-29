using System.IO;
using System.Windows.Threading;

using GihanSoft.AppBase.Bootstrap;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OtakuLib.WFP.Host;

using Serilog;
using Serilog.Core;

namespace OtakuLib.WPF.Host;

public static class Program
{
    [STAThread]
    public static int Main()
    {
        App app;
        Win win;
        ServiceProvider? serviceProvider = null;

        try
        {
            var initializeTask = Task.Run(BackgroundThread);

            app = new();
            app.DispatcherUnhandledException += OnDispatcherUnhandledException;
            app.InitializeComponent();

#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            serviceProvider = initializeTask.Result;
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

            ActivatorUtilities.GetServiceOrCreateInstance<Bootstrap.InitializerUI>(serviceProvider)
                .FullInitialize(serviceProvider);
            win = ActivatorUtilities.GetServiceOrCreateInstance<Win>(serviceProvider);
        }
        catch (Exception ex)
        {
            {
                using var logger = GetShellLogger();
                logger.Error(ex, "startup error");
            }

            serviceProvider?.Dispose();
            throw;
        }

        try
        {
            return app.Run(win);
        }
        catch (Exception ex)
        {
            {
                using var logger = GetShellLogger();
                logger.Error(ex, "runtime error");
            }

            throw;
        }
        finally
        {
            serviceProvider?.Dispose();
        }
    }

    private static Logger GetShellLogger()
    {
        var shellLogPath = @"%AppData%\GihanSoft\OtakuLib\logs\shellLog-.log";

#if DEBUG
        shellLogPath = shellLogPath.Replace("%AppData%", @".\data", StringComparison.Ordinal);
#else
        shellLogPath = Environment.ExpandEnvironmentVariables(shellLogPath);
#endif

        return new LoggerConfiguration()
            .WriteTo.Async(config => config.File(shellLogPath, rollingInterval: RollingInterval.Day))
            .CreateLogger();
    }

    private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Logger.Error(e.Exception, "Dispatcher not handled exception");
    }

    private static ServiceProvider BackgroundThread()
    {
        var configRoot = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"))
#if DEBUG
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.develop.json"))
#endif
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configRoot)
            .CreateLogger();

        ServiceCollection services = new();

        services.AddSingleton<IConfiguration>(configRoot);
        services.AddLogging(builder => builder.AddSerilog(dispose: true));

        var serviceProvider = services.BuildServiceProvider();

        ActivatorUtilities.GetServiceOrCreateInstance<Logic.Bootstrap.ServiceSetup>(serviceProvider).ConfigureServices(services);
        ActivatorUtilities.GetServiceOrCreateInstance<Bootstrap.ServiceSetup>(serviceProvider).ConfigureServices(services);

        serviceProvider.Dispose();
        serviceProvider = services.BuildServiceProvider();

        ActivatorUtilities.GetServiceOrCreateInstance<Logic.Bootstrap.Initializer>(serviceProvider)
            .FullInitialize(serviceProvider);
        ActivatorUtilities.GetServiceOrCreateInstance<Bootstrap.Initializer>(serviceProvider)
            .FullInitialize(serviceProvider);

        return serviceProvider;
    }
}