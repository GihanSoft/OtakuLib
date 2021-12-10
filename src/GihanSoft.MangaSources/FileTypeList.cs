namespace GihanSoft.MangaSources;

public static class FileTypeList
{
    static FileTypeList()
    {
        ImageTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp" };
        CompressedType = new[] { ".zip", ".rar", ".cbr", ".cbz", ".kn" };
    }

    public static IEnumerable<string> ImageTypes { get; }

    public static IEnumerable<string> CompressedType { get; }
}