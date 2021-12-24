namespace OtakuLib.MangaSourceBase;

public abstract class Chapter
{
    public string Id { get; }
    public string Title { get; }
    public string? Volume { get; }

    protected Chapter(string id, string title)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(title);

        Id = id;
        Title = title;
    }

    protected Chapter(string id, string title, string? volume)
        : this(id, title)
    {
        Volume = volume;
    }

    public abstract Task<PagesProvider> GetPagesProviderAsync();
}