using OtakuLib.MangaSourceBase;

namespace GihanSoft.MangaSources;

public class LocalMangaSource : MangaSource
{
    public override string Id { get; } = "GihanSoft.LocalManga";
    public override string Name { get; } = "GihanSoft manga source";

    public override Uri Icon { get; } = new Uri("https://static.thenounproject.com/png/1285340-200.png");
    public override bool AllowDownload { get; }

    public override Task<Manga> GetMangaAsync(string id)
    {
        return Task.FromResult(LocalManga.Ctor(id, null) as Manga);
    }

    public override Task<IEnumerable<Manga>> GetMangasAsync(PaginationInfo paginationInfo)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);

        var result = Directory.EnumerateDirectories(@"D:\Entertainment\Manga")
            .Select(path => LocalManga.Ctor(path, null) as Manga)
            .Skip(paginationInfo.ReceivedMangaCount)
            .Take(20);
        return Task.FromResult(result);
    }

    public override Task<IEnumerable<Manga>> GetMangasAsync(PaginationInfo paginationInfo, params string[] filters)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);

        var result = Directory.EnumerateDirectories(@"D:\Entertainment\Manga")
            .Where(path => Path.GetFileNameWithoutExtension(path).Contains(filters[0], StringComparison.OrdinalIgnoreCase))
            .Select(path => LocalManga.Ctor(path, null) as Manga)
            .Skip(paginationInfo.ReceivedMangaCount)
            .Take(20);
        return Task.FromResult(result);
    }
}