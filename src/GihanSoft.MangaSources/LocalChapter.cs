using OtakuLib.MangaSourceBase;

namespace GihanSoft.MangaSources;

internal sealed class LocalChapter : Chapter
{
    public LocalChapter(string id, string chapter)
        : base(id, chapter)
    {
    }

    public override Task<PagesProvider> GetPagesProviderAsync()
    {
        var isCompressed =
            File.Exists(Id) &&
            FileTypeList.CompressedType.Contains(Path.GetExtension(Id), StringComparer.OrdinalIgnoreCase);
        var pagesProvider = isCompressed ?
            new CompressedPageProvider(Id) :
            new LocalPagesProvider(Id) as PagesProvider;
        return Task.FromResult(pagesProvider);
    }
}