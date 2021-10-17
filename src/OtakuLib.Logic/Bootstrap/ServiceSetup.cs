using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

using GihanSoft.ApplicationFrameworkBase;

using LiteDB;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OtakuLib.Logic.Services;
using OtakuLib.Logic.Utilities;

namespace OtakuLib.Logic.Bootstrap
{
    public class ServiceSetup : IServiceSetup
    {
        private readonly IConfiguration configuration;

        public ServiceSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDatabase(services);
            services.AddSingleton<ISettingsManager, SettingsManager>();

            AddViewModels(services);
        }

        private void AddDatabase(IServiceCollection services)
        {
            var connectionStringSection = configuration.GetSection("connectionString");
            var connectionString = connectionStringSection.Get<ConnectionString>();
            connectionString.Filename = connectionString.Filename.Replace('/', '\\');
            connectionString.Filename = Environment.ExpandEnvironmentVariables(connectionString.Filename);

            new FileInfo(connectionString.Filename).Directory?.EnsureDirectoryExist();

            services
                .AddSingleton<ConnectionString>()
                .AddSingleton<ILiteDatabase, LiteDatabase>()
                .AddSingleton<AppDB>();
        }

        public static void AddViewModels(IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var viewModels = executingAssembly
                .GetReferencedAssemblies()
                .Select(assemblyName => Assembly.Load(assemblyName))
                .SelectMany(assembly => assembly.ExportedTypes)
                .Concat(executingAssembly.DefinedTypes)
                .Where(type => type.IsSubclassOf(typeof(ViewModelBase)));

            foreach (var vm in viewModels)
            {
                var interfaces = vm.FindInterfaces(
                    (itype, _) => itype.GetInterfaces().Contains(typeof(INotifyPropertyChanged)),
                    null);

                switch (interfaces.Length)
                {
                    case 0:
                        services.AddTransient(vm);
                        break;
                    case 1:
                        services.AddTransient(interfaces[0], vm);
                        break;
                    default:
                        foreach (var definition in interfaces)
                        {
                            services.AddTransient(definition, vm);
                        }
                        break;
                }

                foreach (var def in interfaces)
                {
                    if (services.Count(t => t.ServiceType == def) > 1)
                    {
                        throw new Exception();
                    }
                }
            }
        }
    }
}
