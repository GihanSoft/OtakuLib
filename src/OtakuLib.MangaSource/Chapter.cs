namespace OtakuLib.MangaSourceBase;

public abstract class Chapter
{
    public string Id { get; }
    public string? Title { get; }

    protected Chapter(string id)
    {
        Id = id;
    }

    public abstract Task<PagesProvider> GetPagesProviderAsync();
}