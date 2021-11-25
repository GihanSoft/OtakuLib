using System.IO;
using System.Windows.Threading;

using GihanSoft.AppBase;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OtakuLib.WFP;
using OtakuLib.WFP.Host;

namespace OtakuLib.WPF.Host
{
    public static class Program
    {
        [STAThread]
        public static int Main()
        {
            try
            {
                var initializeTask = Task.Run(BackgroundThread);

                App app = new();
                app.DispatcherUnhandledException += OnDispatcherUnhandledException;
                app.InitializeComponent();

                using var serviceProvider = initializeTask
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                ActivatorUtilities.GetServiceOrCreateInstance<Bootstrap.InitializerUI>(serviceProvider)
                    .FullInitialize(serviceProvider);

                var win = ActivatorUtilities.GetServiceOrCreateInstance<Win>(serviceProvider);
                return app.Run(win);
            }
            catch (Exception ex) when (ex is not SystemException)
            {
                HandleException(ex);
                return ex.HResult;
            }
        }

        private static void HandleException(Exception exception)
        {
            throw exception;
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

        private static ServiceProvider BackgroundThread()
        {
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
            ActivatorUtilities.GetServiceOrCreateInstance<Bootstrap.ServiceSetup>(serviceProvider).ConfigureServices(services);

            serviceProvider.Dispose();
            serviceProvider = services.BuildServiceProvider();

            ActivatorUtilities.GetServiceOrCreateInstance<Logic.Bootstrap.Initializer>(serviceProvider)
                .FullInitialize(serviceProvider);
            ActivatorUtilities.GetServiceOrCreateInstance<Bootstrap.Initializer>(serviceProvider)
                .FullInitialize(serviceProvider);

            return serviceProvider;
        }

        private static void FullInitialize(this IInitializer initializer, ServiceProvider serviceProvider)
        {
            var conditionProvider = serviceProvider.GetRequiredService<IInitializeConditionProvider>();
            if (conditionProvider.IsFirstRun())
            {
                initializer.FirstRunInitialize();
            }

            if (conditionProvider.IsUpdate())
            {
                initializer.UpdateInitialize();
            }

            initializer.Initialize();

            _ = Task.Run(() => initializer.LateInitialize()).ConfigureAwait(false);
        }
    }
}
