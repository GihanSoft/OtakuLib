namespace OtakuLib.Logic.Models;

public record LibMangaChapter
{
    public LibMangaChapter(string id, string title, int status)
    {
        Id = id;
        Title = title;
        Status = status;
    }

    public string Id { get; init; }
    public string Title { get; init; }
    public int Status { get; set; }
}