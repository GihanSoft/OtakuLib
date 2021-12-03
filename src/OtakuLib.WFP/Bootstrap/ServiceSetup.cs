using System.Reflection;

using GihanSoft.AppBase.Bootstrap;
using GihanSoft.Navigation.Abstraction;
using GihanSoft.Navigation.WPF;

using Microsoft.Extensions.DependencyInjection;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Services;
using OtakuLib.WPF.Components;
using OtakuLib.WPF.Pages.PgMainTabs;
using OtakuLib.WPF.Providers;
using OtakuLib.WPF.ViewModels;

namespace OtakuLib.WPF.Bootstrap;

public class ServiceSetup : IServiceSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<WinVM>();
        services.AddScoped<PageNavigator>();
        services.AddScoped<IPageNavigator>(serviceProvider => serviceProvider.GetRequiredService<PageNavigator>());

        AddPages(services);
        AddPagesViewers(services);
        services.AddTransient<PgTabBrowse>();
        AddProviders(services);
    }

    private static void AddProviders(IServiceCollection services)
    {
        services.AddSingleton<IFullScreenProvider, FullScreenProvider>();
    }

    private static void AddPagesViewers(IServiceCollection services)
    {
        services.AddTransient<IPagesViewer, SinglePagePagesViewer>();
    }

    private static void AddPages(IServiceCollection services)
    {
        var pages = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Page)));

        foreach (var page in pages)
        {
            var definition =
                page.GetInterfaces().Single(iface => iface.GetInterfaces().Contains(typeof(IPage)));
            services.AddTransient(definition, page);
        }
    }
}
