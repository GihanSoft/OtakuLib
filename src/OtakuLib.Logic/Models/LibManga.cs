using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.Models;

public class LibManga
{
    public string? SourceId { get; set; }
    public string? Id { get; set; }
    public Manga? Manga { get; set; }
    public Uri? Cover { get; set; }
    public string? Title { get; set; }
    public ICollection<LibMangaChapter>? Chapters { get; init; }
}
