namespace OtakuLib.Logic.Models.Settings;

public record MangaLibSettings
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MangaLibSettings() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public string? DefaultMangaViewerId { get; init; }

    private readonly static Lazy<MangaLibSettings> lazyDefault = new(GetDefault);
    public static MangaLibSettings Default => lazyDefault.Value;
    private static MangaLibSettings GetDefault()
    {
        return new()
        {
            DefaultMangaViewerId = null,
        };
    }
}