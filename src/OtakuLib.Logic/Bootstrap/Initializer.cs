
using GihanSoft.AppBase;
using GihanSoft.AppBase.Exceptions;

using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Services;

namespace OtakuLib.Logic.Bootstrap;

public class Initializer : IInitializer
{
    private readonly ISettingsManager<MainSettings> settingsManager;
    private readonly Version version;

    public Initializer(
        ISettingsManager<MainSettings> settingsManager,
        Version version)
    {
        this.settingsManager = settingsManager;
        this.version = version;
    }

    public void FirstRunInitialize()
    {
        settingsManager.Save(MainSettings.Default with
        {
            Version = version.ToString(4)
        });
    }

    public void Initialize()
    {
    }

    public void LateInitialize()
    {
    }

    public void UpdateInitialize()
    {
        var mainSettings = settingsManager.Fetch();
        settingsManager.Save(mainSettings with
        {
            Version = version.ToString(4)
        });
    }
}
