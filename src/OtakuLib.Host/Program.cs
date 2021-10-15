using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Threading;

using GihanSoft.ApplicationFrameworkBase;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OtakuLib.View;

namespace OtakuLib.Host
{
    public static class Program
    {
        [STAThread]
        public static int Main()
        {
            var initializeTask = Task.Run(BackgroundThread);

            App app = new();
            app.DispatcherUnhandledException += OnDispatcherUnhandledException;
            app.InitializeComponent();

            using var serviceProvider = initializeTask
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            ActivatorUtilities.GetServiceOrCreateInstance<View.Bootstrap.InitializerUI>(serviceProvider).FullInitialize();

            var win = ActivatorUtilities.GetServiceOrCreateInstance<Win>(serviceProvider);
            return app.Run(win);
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            /** TODO:
             * log
             * show
             * prevent crash
             */
        }

        private static ServiceProvider BackgroundThread()
        {
            SetNecessaryEnvironments();

            var configRoot = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"))
#if DEBUG
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.develop.json"))
#endif
                .Build();

            ServiceCollection services = new();

            services.AddSingleton<IConfiguration>(configRoot);

            var serviceProvider = services.BuildServiceProvider();

            ActivatorUtilities.GetServiceOrCreateInstance<Logic.Bootstrap.ServiceSetup>(serviceProvider).ConfigureServices(services);
            ActivatorUtilities.GetServiceOrCreateInstance<View.Bootstrap.ServiceSetup>(serviceProvider).ConfigureServices(services);

            ActivatorUtilities.GetServiceOrCreateInstance<Logic.Bootstrap.Initializer>(serviceProvider).FullInitialize();
            ActivatorUtilities.GetServiceOrCreateInstance<View.Bootstrap.Initializer>(serviceProvider).FullInitialize();

            return serviceProvider;
        }

        private static void SetNecessaryEnvironments()
        {
            var exeLocation =
                Assembly.GetEntryAssembly()?.Location ??
                Assembly.GetExecutingAssembly().Location;

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(exeLocation);

            var companyName = fileVersionInfo.CompanyName ?? "GihanSoft";
            var productName = fileVersionInfo.ProductName ?? "MangaReader";

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var currentAppData = Path.Combine(appDataPath, companyName, productName);

            Environment.SetEnvironmentVariable(
                nameof(companyName),
                companyName,
                EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(
                nameof(productName),
                productName,
                EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(
                nameof(currentAppData),
                currentAppData,
                EnvironmentVariableTarget.Process);
        }

        private static void FullInitialize(this IInitializer initializer)
        {
            initializer.FirstRunInitialize();
            initializer.UpdateInitialize();
            initializer.Initialize();
        }
    }
}
