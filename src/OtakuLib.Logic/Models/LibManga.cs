using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.Models;

public class LibManga
{
    private readonly Manga? manga;

    public LibManga(Manga manga)
    {
        ArgumentNullException.ThrowIfNull(manga);
        this.manga = manga;
        Id = manga.Id;
        Cover = manga.Cover;
        Title = manga.Title;
    }

    public LibManga(string sourceId, string id, Uri cover, string title)
    {
        SourceId = sourceId;
        Id = id;
        Cover = cover;
        Title = title;
    }

    public string? SourceId { get; }
    public string Id { get; }
    public Uri Cover { get; }
    public string Title { get; }
}
