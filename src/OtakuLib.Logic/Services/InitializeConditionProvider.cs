using GihanSoft.AppBase.Bootstrap;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Models.Settings;

namespace OtakuLib.Logic.Services;

public class InitializeConditionProvider : IInitializeConditionProvider
{
    private readonly MainSettings? mainSettings;
    private readonly Version version;

    public InitializeConditionProvider(IDataManager<MainSettings> settingsManager, Version version)
    {
        ArgumentNullException.ThrowIfNull(settingsManager);
        settingsManager.TryFetch(out mainSettings);
        this.version = version;
    }

    public bool IsFirstRun()
    {
        return mainSettings is null;
    }

    public bool IsUpdate()
    {
        return mainSettings?.Version != version.ToString(4);
    }
}
