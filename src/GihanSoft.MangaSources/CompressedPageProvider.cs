using GihanSoft.String;

using OtakuLib.MangaSourceBase;

using SharpCompress.Readers;

namespace GihanSoft.MangaSources;

/// <summary>
/// Provides pages within compressed file (.zip, .rar, etc).
/// </summary>
internal class CompressedPageProvider : PagesProvider
{
    private static string[] GetPageNames(Stream stream)
    {
        stream.Position = 0;
        using var reader = ReaderFactory.Open(stream);
        List<string> pageNames = new();
        while (reader.MoveToNextEntry())
        {
            if (!reader.Entry.IsDirectory && FileTypeUtility.IsImage(reader.Entry.Key))
            {
                pageNames.Add(reader.Entry.Key);
            }
        }

        return pageNames.NaturalOrderBy().ToArray();
    }

    private readonly string[] pageNames;
    private readonly MemoryStream?[] loadedPages;

    private readonly bool ownStream;
    private readonly bool tempStream;
    private readonly Func<Stream> getStream;

    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompressedPageProvider"/> class.
    /// </summary>
    /// <param name="stream">Compressed file stream.</param>
    /// <param name="ownStream"><see langword="true"/> if <see cref="CompressedPageProvider"/> own stream and should dispose it.</param>
    public CompressedPageProvider(Stream stream, bool ownStream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        tempStream = false;
        this.ownStream = ownStream;
        getStream = () => stream;

        pageNames = GetPageNames(stream);
        loadedPages = new MemoryStream[pageNames.Length];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompressedPageProvider"/> class.
    /// </summary>
    /// <param name="filePath">Compressed file path.</param>
    public CompressedPageProvider(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);

        tempStream = true;
        ownStream = true;
        getStream = () => File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        using var stream = getStream();
        pageNames = GetPageNames(stream);
        loadedPages = new MemoryStream[pageNames.Length];
    }

    /// <summary>
    /// Gets pages count.
    /// </summary>
    public override int Count => disposed ? throw new ObjectDisposedException(nameof(CompressedPageProvider)) : pageNames.Length;

    public override MemoryStream? GetPage(int page)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(LocalMangaSource));
        }

        return loadedPages[page];
    }

    /// <summary>
    /// Load given page into memory stream.
    /// </summary>
    /// <param name="page">page number (0-base).</param>
    /// <returns>Task of <see langword="async"/> function.</returns>
    public override Task LoadPageAsync(int page, CancellationToken cancellationToken = default)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(LocalMangaSource));
        }

        return LoadPageAsync(page, null, cancellationToken);
    }

    public override async Task LoadPageAsync(int page, IProgress<double>? progress, CancellationToken cancellationToken = default)
    {
        if (loadedPages[page] is not null)
        {
            return;
        }

        var name = pageNames[page];
        var stream = getStream();
        try
        {
            stream.Position = 0;

            using var reader = ReaderFactory.Open(stream);
            while (reader.MoveToNextEntry() && !reader.Entry.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
            }

            progress?.Report(0.25);

            var memStream = new MemoryStream();
            reader.WriteEntryTo(memStream);
            loadedPages[page] = memStream;
            progress?.Report(1);
        }
        finally
        {
            if (tempStream)
            {
                await stream.DisposeAsync().ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Release memory of given page.
    /// </summary>
    /// <param name="page">Page number (0-base).</param>
    /// <returns>Task of <see langword="async"/> function.</returns>
    public override Task UnLoadPageAsync(int page)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(LocalMangaSource));
        }

        var mem = loadedPages[page];
        if (mem is null)
        {
            return Task.CompletedTask;
        }

        loadedPages[page] = null;
        return mem.DisposeAsync().AsTask();
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        disposed = true;
        if (disposing)
        {
            if (ownStream && !tempStream)
            {
                getStream().Dispose();
            }

            foreach (var item in loadedPages)
            {
                item?.Dispose();
            }
        }
    }
}