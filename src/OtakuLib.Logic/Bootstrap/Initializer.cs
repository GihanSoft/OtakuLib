using GihanSoft.AppBase.Bootstrap;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Models.Settings;

namespace OtakuLib.Logic.Bootstrap;

public class Initializer : IInitializer
{
    private readonly IDataManager<AppData> dataProvider;
    private readonly Version version;

    public Initializer(
        IDataManager<AppData> dataProvider,
        Version version)
    {
        this.dataProvider = dataProvider;
        this.version = version;
    }

    public void FirstRunInitialize()
    {
        dataProvider.Save(AppData.Default with
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
        var data = dataProvider.Fetch();
        dataProvider.Save(data with
        {
            Version = version.ToString(4)
        });
    }
}