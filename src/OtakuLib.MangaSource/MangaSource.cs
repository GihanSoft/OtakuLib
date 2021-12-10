namespace OtakuLib.MangaSourceBase;

public abstract class MangaSource
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract Uri Icon { get; }

    public abstract Task<Manga> GetMangaAsync(string id);

    public abstract Task<IEnumerable<Manga>> GetMangasAsync(PaginationInfo paginationInfo);

    public abstract Task<IEnumerable<Manga>> GetMangasAsync(PaginationInfo paginationInfo, params string[] filters);
}