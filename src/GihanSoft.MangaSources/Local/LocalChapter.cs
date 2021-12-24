using OtakuLib.MangaSourceBase;

namespace GihanSoft.MangaSources;

internal sealed class LocalChapter : Chapter
{
    public LocalChapter(string id, string title)
        : base(id, title)
    {
    }

    public override Task<PagesProvider> GetPagesProviderAsync()
    {
        var isCompressed =
            File.Exists(Id) &&
            FileTypeUtility.CompressedExtensions.Contains(Path.GetExtension(Id), StringComparer.OrdinalIgnoreCase);

        var pagesProvider = isCompressed ?
            new CompressedPageProvider(Id) :
            new LocalPagesProvider(Id) as PagesProvider;

        return Task.FromResult(pagesProvider);
    }
}