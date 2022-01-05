namespace OtakuLib.Logic.Models.Settings;

public record AppData
{
    public const string Key = "OtakuLib.MainSettings";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AppData() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MangaLibSettings MangaLibSettings { get; init; }

    public string Version { get; init; }

    private readonly static Lazy<AppData> lazyDefault = new(GetDefault);
    public static AppData Default => lazyDefault.Value;
    private static AppData GetDefault()
    {
        return new()
        {
            Version = "0.0.0.0",
            MangaLibSettings = MangaLibSettings.Default,
        };
    }
}