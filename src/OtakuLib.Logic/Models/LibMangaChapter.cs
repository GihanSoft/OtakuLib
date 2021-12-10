using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.Models;

public class LibMangaChapter
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Status { get; set; }

    public Chapter? Chapter { get; set; }

    public PagesProvider GetPagesProvider()
    {
        return Chapter?.GetPagesProviderAsync().ConfigureAwait(true).GetAwaiter().GetResult();
    }
}