using GihanSoft.String;

using OtakuLib.MangaSourceBase;

namespace GihanSoft.MangaSources;

internal sealed class LocalManga : Manga
{
    private LocalManga(string id, string title, Uri cover)
        : base(id, title, cover)
    {
    }

    public override Task<Chapter> GetChapterAsync(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<IEnumerable<Chapter>> GetChaptersAsync()
    {
        var result = Directory.EnumerateFileSystemEntries(Id)
            .Where(e =>
                Directory.Exists(e) ||
                FileTypeUtility.CompressedExtensions.Contains(Path.GetExtension(e), StringComparer.OrdinalIgnoreCase))
            .NaturalOrderBy(x => x)
            .Select(dirPath => new LocalChapter(dirPath, Path.GetFileName(dirPath)) as Chapter);

        return Task.FromResult(result);
    }

    public static LocalManga Ctor(string path, Uri? cover)
    {
        ArgumentNullException.ThrowIfNull(path);
        var id = path;
        var title = Path.GetFileName(path);
        if (cover is null)
        {
            var files = Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
            var coverPath = files.FirstOrDefault(path =>
                Path.GetFileNameWithoutExtension(path).Equals("cover", StringComparison.OrdinalIgnoreCase));
            coverPath ??= files.FirstOrDefault(path =>
                path.Contains("cover", StringComparison.OrdinalIgnoreCase));
            coverPath ??= files.FirstOrDefault(path =>
                Path.GetExtension(path).Equals(".JPG", StringComparison.OrdinalIgnoreCase));
        }

        cover ??= new Uri("https://static.thenounproject.com/png/1285340-200.png");

        return new LocalManga(id, title, cover);
    }
}