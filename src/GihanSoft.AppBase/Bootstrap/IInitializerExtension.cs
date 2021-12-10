using Microsoft.Extensions.DependencyInjection;

namespace GihanSoft.AppBase.Bootstrap;

public static class IInitializerExtension
{
    public static void FullInitialize(this IInitializer initializer, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(initializer);

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