namespace OtakuLib.MangaSourceBase;

public abstract class Chapter
{
    public string Id { get; }
    public string Title { get; }
    public string? Volume { get; }

    protected Chapter(string id, string title)
    {
        Id = id;
        Title = title;
    }

    public abstract Task<PagesProvider> GetPagesProviderAsync();
}