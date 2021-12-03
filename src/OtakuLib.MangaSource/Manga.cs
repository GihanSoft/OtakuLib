namespace OtakuLib.MangaSourceBase;

public abstract class Manga
{
    protected Manga(string id, string title, Uri cover)
    {
        Id = id;
        Title = title;
        Cover = cover;
    }

    public string Id { get; }
    public string Title { get; }
    public Uri Cover { get; }

    public abstract Task<Chapter> GetChapterAsync(string id);

    public abstract Task<IEnumerable<Chapter>> GetChaptersAsync();
}