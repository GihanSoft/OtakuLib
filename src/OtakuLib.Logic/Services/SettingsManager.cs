using System.Diagnostics.CodeAnalysis;

using GihanSoft.AppBase.Exceptions;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.Services;

[SuppressMessage("Build", "CA1812:never instantiated", Justification = "auto build service.")]
internal class SettingsManager<TSetting> : IDataManager<TSetting>
    where TSetting : class
{
    private readonly AppDB db;

    private readonly string key;
    private readonly object locker = new();

    private TSetting? cache;

    public SettingsManager(AppDB db)
    {
        this.db = db;

        key = typeof(TSetting).FullName ?? throw new ArgumentException("setting type is not valid.");
    }

    public bool TryFetch(out TSetting? setting)
    {
        StringArgExceptionHelper.ThrowIfNullOrWhiteSpace(key);

        lock (locker)
        {
            if (cache is null)
            {
                var doc = db.Settings.FindOne(s => s.Id == key)?.Value;
                if (doc is null)
                {
                    setting = null;
                    return false;
                }

                cache = db.Database.Mapper.Deserialize<TSetting>(doc);
            }

            setting = cache;
            return true;
        }
    }

    public TSetting Fetch()
    {
        var success = TryFetch(out var setting);

        if (success)
        {
            return setting!;
        }

        throw new UnExpectedNullException("requested setting not found. use 'TryFetch' if you're not sure of setting existence.");
    }

    public void Save(TSetting setting)
    {
        StringArgExceptionHelper.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(setting);

        Setting settings = new(key, db.Database.Mapper.Serialize(setting));
        lock (locker)
        {
            _ = db.Settings.Upsert(settings);
            cache = setting;
        }
    }
}
