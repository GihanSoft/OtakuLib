using LiteDB;

using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.Services;

internal class SettingsManager : ISettingsManager
{
    private readonly AppDB db;
    private readonly Dictionary<string, object> cache = new();
    private readonly object locker = new();

    public SettingsManager(AppDB db)
    {
        this.db = db;
    }

    public TSetting? Fetch<TSetting>(string key)
        where TSetting : class
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
        }

        lock (locker)
        {
            if (!cache.TryGetValue(key, out var value))
            {
                var doc = db.Settings.FindOne(s => s.Id == key)?.Value;
                if (doc is null) { return null; }

                value = db.Database.Mapper.Deserialize<TSetting>(doc);
                cache[key] = value;
            }

            return (TSetting)value;
        }
    }

    public void Save<TOptions>(string key, TOptions setting)
        where TOptions : class
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException($"'{nameof(key)}' cannot be null or white space.", nameof(key));
        }

        ArgumentNullException.ThrowIfNull(setting);

        Setting settings = new(key, db.Database.Mapper.Serialize(setting));
        lock (locker)
        {
            var found = db.Settings.Update(settings);
            if (!found)
            {
                _ = db.Settings.Insert(settings);
            }

            cache[key] = setting;
        }
    }
}

public interface ISettingsManager
{
    TSetting? Fetch<TSetting>(string key) where TSetting : class;
    void Save<TSetting>(string key, TSetting setting) where TSetting : class;
}
