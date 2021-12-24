using System.Collections.Immutable;

namespace OtakuLib.MangaSourceBase;

public class SimpleHttpPagesProvider : PagesProvider
{
    private readonly ImmutableArray<Uri> links;
    private readonly MemoryStream?[] loadedPages;

    private readonly HttpClient httpClient;

    private bool disposed;

    public SimpleHttpPagesProvider(IEnumerable<Uri> pages)
    {
        ArgumentNullException.ThrowIfNull(pages);

        links = pages.ToImmutableArray();
        loadedPages = new MemoryStream[links.Length];

        httpClient = new();
    }

    public SimpleHttpPagesProvider(IEnumerable<string> pages)
        : this(pages.Select(uri => new Uri(uri)))
    {
    }

    public override int Count => disposed ? throw new ObjectDisposedException(nameof(SimpleHttpPagesProvider)) : links.Length;

    public override MemoryStream? GetPage(int page)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(SimpleHttpPagesProvider));
        }

        return loadedPages[page];
    }

    public override Task LoadPageAsync(int page, CancellationToken cancellationToken = default)
    {
        return InternalLoadPageAsync(page, null, cancellationToken);
    }

    public override Task LoadPageAsync(
        int page,
        IProgress<double>? progress,
        CancellationToken cancellationToken = default)
    {
        return InternalLoadPageAsync(page, progress, cancellationToken);
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
            httpClient.Dispose();
            foreach (var loaded in loadedPages)
            {
                loaded?.Dispose();
            }
        }
    }

    private async Task InternalLoadPageAsync(
        int page,
        IProgress<double>? progress,
        CancellationToken cancellationToken = default)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(SimpleHttpPagesProvider));
        }

        if (loadedPages[page] is not null)
        {
            return;
        }

        using var response = await httpClient.GetAsync(
            links[page],
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken)
            .ConfigureAwait(false);
        var contentLength = response.Content.Headers.ContentLength;

        using var netStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        MemoryStream memStream = new();
        if (progress == null || !contentLength.HasValue)
        {
            await netStream.CopyToAsync(memStream, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            var buffer = new byte[contentLength.Value];
            var readBytesCount = 0;
            while (readBytesCount < contentLength.Value)
            {
                readBytesCount += await netStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                progress.Report((double)readBytesCount / contentLength.Value);
            }

            await memStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
        }

        loadedPages[page] = memStream;
        progress?.Report(1);
    }
}