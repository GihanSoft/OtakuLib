using System.Collections.Immutable;

namespace GihanSoft.MangaSources;

public static class FileTypeList
{
    static FileTypeList()
    {
        ImageTypes = ImmutableArray.Create(".JPG", ".JPEG", ".PNG", ".BMP", ".GIF", ".WEBP");
        CompressedType = ImmutableArray.Create(".ZIP", ".RAR", ".CBR", ".CBZ");
    }

    public static IEnumerable<string> ImageTypes { get; }

    public static IEnumerable<string> CompressedType { get; }
}