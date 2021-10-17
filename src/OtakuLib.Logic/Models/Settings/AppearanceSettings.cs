namespace OtakuLib.Logic.Models.Settings
{
    public record AppearanceSettings
    {
        public int WindowState { get; init; }
        public double Top { get; init; }
        public double Left { get; init; }
        public double Height { get; init; }
        public double Width { get; init; }
    }
}
