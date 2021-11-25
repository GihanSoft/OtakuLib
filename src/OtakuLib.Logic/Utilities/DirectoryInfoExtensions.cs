namespace OtakuLib.Logic.Utilities;

internal static class DirectoryInfoExtensions
{
    public static void EnsureDirectoryExist(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);

        if (directoryInfo.Exists) { return; }

        if (directoryInfo.Parent?.Exists == false) //also check null
        {
            EnsureDirectoryExist(directoryInfo.Parent);
        }

        directoryInfo.Create();
    }
}
