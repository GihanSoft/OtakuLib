using System.Collections.Immutable;

namespace OtakuLib.MangaSourceBase;

public abstract class Manga
{
    protected Manga(string id, string title, Uri cover, params KeyValuePair<string, string>[] infos)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(cover);

        Id = id;
        Title = title;
        Cover = cover;
        Infos = infos.ToImmutableArray();
    }

    public string Id { get; }
    public string Title { get; }
    public Uri Cover { get; }
    public IEnumerable<KeyValuePair<string, string>> Infos { get; }

    public abstract Task<Chapter> GetChapterAsync(string id);

    public abstract Task<IEnumerable<Chapter>> GetChaptersAsync();
}