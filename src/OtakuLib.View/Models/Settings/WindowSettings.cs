using System.Windows;

namespace OtakuLib.View.Models.Settings
{
    public record WindowSettings
    {
        public const string Key = "{88169CBB-E10B-4571-A0DC-8837BD0E9EBB}";

        public WindowState WindowState { get; init; }
        public double Top { get; init; }
        public double Left { get; init; }
        public double Height { get; init; }
        public double Width { get; init; }
    }
}
