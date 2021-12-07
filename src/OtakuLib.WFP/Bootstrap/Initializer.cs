using GihanSoft.AppBase.Bootstrap;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Models.Settings;

namespace OtakuLib.WPF.Bootstrap;

public class Initializer : IInitializer
{
    private readonly IDataManager<MainSettings> settingsManager;

    public Initializer(IDataManager<MainSettings> settingsManager)
    {
        this.settingsManager = settingsManager;
    }

    public void FirstRunInitialize()
    {
    }

    public void Initialize()
    {
    }

    public void LateInitialize()
    {
    }

    public void UpdateInitialize()
    {
    }
}
