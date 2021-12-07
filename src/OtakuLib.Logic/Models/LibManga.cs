using OtakuLib.MangaSourceBase;

using System.Collections.ObjectModel;

namespace OtakuLib.Logic.Models;

public class LibManga
{
    private readonly ObservableCollection<LibMangaChapter> chapters = new();
    private readonly ReadOnlyObservableCollection<LibMangaChapter> readOnlyChapters;

    public LibManga()
    {
        chapters = new();
        readOnlyChapters = new(chapters);
    }

    public string? SourceId { get; set; }
    public string? Id { get; set; }
    public Manga? Manga { get; set; }
    public Uri? Cover { get; set; }
    public string? Title { get; set; }
    public ReadOnlyObservableCollection<LibMangaChapter> Chapters
    {
        get
        {
            if (readOnlyChapters.Count == 0)
            {
                RefreshAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            }

            return readOnlyChapters;
        }
    }

    public string? PagesViewerId { get; set; }

    public static LibManga BlankLibManga { get; } = new();

    public async Task RefreshAsync()
    {
        chapters.Clear();
        if (Manga is not null)
        {
            var newChapters = await Manga.GetChaptersAsync().ConfigureAwait(true);
            foreach (var chapter in newChapters)
            {
                chapters.Add(new LibMangaChapter() { Chapter = chapter, Title = chapter.Title });
            }
        }
    }
}
