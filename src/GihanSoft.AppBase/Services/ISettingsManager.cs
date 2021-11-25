namespace OtakuLib.Logic.Services;

public interface ISettingsManager<TSetting>
    where TSetting : class
{
    TSetting Fetch();
    bool TryFetch(out TSetting? setting);
    void Save(TSetting setting);
}
