using System.Collections.ObjectModel;

namespace OtakuLib.Logic.Models;

public class LibManga
{
    public string SourceId { get; init; }
    public string Id { get; init; }

    public Uri? Cover { get; set; }
    public string? Title { get; set; }

    public string? PagesViewerId { get; set; }

    public ObservableCollection<LibMangaChapter> Chapters { get; init; } = new();
}