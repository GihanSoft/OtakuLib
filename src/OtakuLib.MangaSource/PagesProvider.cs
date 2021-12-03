namespace OtakuLib.MangaSourceBase;

public abstract class PagesProvider : IDisposable
{
    /// <summary>
    /// give loaded page as <see cref="MemoryStream"/> or <see cref="null"/> if not loaded.
    /// </summary>
    /// <param name="page">page number (start from 0).</param>
    public MemoryStream? this[int page] => GetPage(page);

    /// <summary>
    /// Gets pages count.
    /// </summary>
    public abstract int Count { get; }

    /// <summary>
    /// give loaded page as <see cref="MemoryStream"/> or <see cref="null"/> if not loaded.
    /// </summary>
    /// <param name="page">page number (start from 0).</param>
    public abstract MemoryStream? GetPage(int page);

    /// <summary>
    /// load page from source (web, storage, etc) to <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="page">page number (start from 0).</param>
    public abstract Task LoadPageAsync(int page);

    /// <summary>
    /// unload page from ram to optimize ram usage. better to cache it on storage
    /// </summary>
    /// <param name="page">page number (start from 0).</param>
    public abstract Task UnLoadPageAsync(int page);

    protected abstract void Dispose(bool disposing);

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
