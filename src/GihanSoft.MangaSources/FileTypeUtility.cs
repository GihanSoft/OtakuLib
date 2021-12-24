using System.Collections.Immutable;

namespace GihanSoft.MangaSources;

public static class FileTypeUtility
{
    public static IEnumerable<string> ImageExtensions { get; } =
        ImmutableArray.Create(".JPG", ".JPEG", ".PNG", ".BMP", ".GIF", ".WEBP");

    public static IEnumerable<string> CompressedExtensions { get; } =
        ImmutableArray.Create(".ZIP", ".RAR", ".CBR", ".CBZ");

    public static bool IsImage(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        return ImageExtensions.Contains(Path.GetExtension(filePath), StringComparer.InvariantCultureIgnoreCase);
    }

    public static bool IsCompressed(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        return CompressedExtensions.Contains(Path.GetExtension(filePath), StringComparer.InvariantCultureIgnoreCase);
    }

    public static bool IsImage(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo);
        return ImageExtensions.Contains(fileInfo.Extension, StringComparer.InvariantCultureIgnoreCase);
    }

    public static bool IsCompressed(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo);
        return CompressedExtensions.Contains(fileInfo.Extension, StringComparer.InvariantCultureIgnoreCase);
    }
}