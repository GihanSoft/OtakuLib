namespace OtakuLib.Logic.Models.Settings
{
    public record MainSettings
    {
        public const string Key = "{10FD60D0-546C-4FDC-9D92-849E571C4C7E}";

        public AppearanceSettings AppearanceSettings { get; init; }
        public string Version { get; init; }
    }
}
