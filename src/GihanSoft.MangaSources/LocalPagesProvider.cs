using GihanSoft.String;

using OtakuLib.MangaSourceBase;

namespace GihanSoft.MangaSources;

public class LocalPagesProvider : PagesProvider
{
    private readonly string path;
    private readonly string[] files;
    private readonly MemoryStream?[] loadedPages;

    private bool disposed;

    public LocalPagesProvider(string path)
    {
        this.path = path;
        files = Directory.GetFiles(path, "*", SearchOption.AllDirectories)
            .Where(FileTypeUtility.IsImage)
            .NaturalOrderBy()
            .ToArray();
        loadedPages = new MemoryStream[files.Length];
    }

    public override int Count => disposed ? throw new ObjectDisposedException(nameof(LocalPagesProvider)) : files.Length;

    public override MemoryStream? GetPage(int page)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(LocalMangaSource));
        }

        return loadedPages[page];
    }

    public override async Task LoadPageAsync(int page, CancellationToken cancellationToken = default)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(LocalMangaSource));
        }

        var bin = await File.ReadAllBytesAsync(files[page], cancellationToken).ConfigureAwait(false);
        loadedPages[page] = new MemoryStream(bin);
    }

    public override async Task LoadPageAsync(int page, IProgress<double> progress, CancellationToken cancellationToken = default)
    {
        await LoadPageAsync(page, cancellationToken).ConfigureAwait(false);
        progress?.Report(1);
    }

    public override Task UnLoadPageAsync(int page)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(SimpleHttpPagesProvider));
        }

        var mem = loadedPages[page];
        if (mem is null)
        {
            return Task.CompletedTask;
        }

        loadedPages[page] = null;
        return mem.DisposeAsync().AsTask();
    }

    protected override void Dispose(bool disposing)
    {
        disposed = true;
        if (disposing)
        {
            foreach (var stream in loadedPages)
            {
                stream?.Dispose();
            }
        }
    }
}