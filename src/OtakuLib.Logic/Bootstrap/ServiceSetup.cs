using System.ComponentModel;
using System.Reflection;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Bootstrap;
using GihanSoft.AppBase.Exceptions;
using GihanSoft.AppBase.Services;
using GihanSoft.MangaSources;

using LiteDB;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OtakuLib.Logic.Services;
using OtakuLib.Logic.Utilities;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.Bootstrap;

public class ServiceSetup : IServiceSetup
{
    private readonly IConfiguration configuration;

    public ServiceSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IInitializeConditionProvider, InitializeConditionProvider>();
        AddVersion(services);
        AddDatabase(services);
        services.AddSingleton(typeof(IDataManager<>), typeof(SettingsManager<>));
        AddViewModels(services);
        services.AddSingleton<MangaSource, LocalMangaSource>();
    }

    public static void AddVersion(IServiceCollection services)
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version ?? throw new UnExpectedNullException();
        services.AddSingleton(version);
    }

    private void AddDatabase(IServiceCollection services)
    {
        var connectionStringSection = configuration.GetSection("connectionString");
        var connectionString = connectionStringSection.Get<ConnectionString>();
        connectionString.Filename = connectionString.Filename.Replace('/', '\\');
        connectionString.Filename = Environment.ExpandEnvironmentVariables(connectionString.Filename);

        new FileInfo(connectionString.Filename).Directory?.EnsureDirectoryExist();

        services
            .AddSingleton(connectionString)
            .AddSingleton(BsonMapper.Global)
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
                    _ = services.AddTransient(vm);
                    break;

                case 1:
                    _ = services.AddTransient(interfaces[0], vm);
                    break;

                default:
                    foreach (var definition in interfaces)
                    {
                        _ = services.AddTransient(definition, vm);
                    }

                    break;
            }

            foreach (var def in interfaces)
            {
                if (services.Count(t => t.ServiceType == def) > 1)
                {
                    throw new UnExpectedException("two imp for an interface.");
                }
            }
        }
    }
}