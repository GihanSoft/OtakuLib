using GihanSoft.String;

using OtakuLib.MangaSourceBase;

namespace GihanSoft.MangaSources;

public class LocalPagesProvider : PagesProvider
{
    private readonly string path;
    private readonly string[] files;
    private readonly MemoryStream?[] memoryStreams;

    private bool disposed;

    public LocalPagesProvider(string path)
    {
        disposed = false;
        this.path = path;
        files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        Array.Sort(files, NaturalComparer.OrdinalIgnoreCase);
        memoryStreams = new MemoryStream[files.Length];
    }

    public override int Count => disposed ? throw new ObjectDisposedException(nameof(LocalMangaSource)) : files.Length;

    public override MemoryStream? GetPage(int page)
    {
        if (disposed)
        { throw new ObjectDisposedException(nameof(LocalMangaSource)); }

        return memoryStreams[page];
    }

    public override async Task LoadPageAsync(int page)
    {
        var bin = await File.ReadAllBytesAsync(files[page]).ConfigureAwait(false);
        memoryStreams[page] = new MemoryStream(bin);
    }

    public override async Task UnLoadPageAsync(int page)
    {
        var memoryStream = memoryStreams[page];
        if (memoryStream is not null)
        {
            await memoryStream.DisposeAsync().ConfigureAwait(false);
            memoryStreams[page] = null;
        }
    }

    protected override void Dispose(bool disposing)
    {
        disposed = true;
        if (disposing)
        {
            foreach (var stream in memoryStreams)
            {
                stream?.Dispose();
            }
        }
    }
}