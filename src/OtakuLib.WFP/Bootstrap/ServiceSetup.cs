using System.Reflection;

using GihanSoft.AppBase.Bootstrap;
using GihanSoft.Navigation.Abstraction;
using GihanSoft.Navigation.WPF;

using Microsoft.Extensions.DependencyInjection;

using OtakuLib.Logic.Components;
using OtakuLib.WPF.Components;
using OtakuLib.WPF.Pages.PgMainTabs;
using OtakuLib.WPF.ViewModels;

namespace OtakuLib.WPF.Bootstrap;

public class ServiceSetup : IServiceSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<WinVM>();
        services.AddScoped<IPageNavigator, PageNavigator>();
        services.AddScoped(serviceProvider => serviceProvider.GetService<IPageNavigator>() as PageNavigator);

        AddPages(services);
        AddPagesViewers(services);
        services.AddTransient<PgTabBrowse>();
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
