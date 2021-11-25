using GihanSoft.AppBase;

using OtakuLib.Logic.Models.Settings;

namespace OtakuLib.Logic.Services
{
    public class InitializeConditionProvider : IInitializeConditionProvider
    {
        private readonly MainSettings? mainSettings;
        private readonly Version version;

        public InitializeConditionProvider(ISettingsManager<MainSettings> settingsManager, Version version)
        {
            ArgumentNullException.ThrowIfNull(settingsManager);
            mainSettings = settingsManager.Fetch();
            this.version = version;
        }

        public bool IsFirstRun()
        {
            return mainSettings is null;
        }

        public bool IsUpdate()
        {
            return mainSettings?.Version != version.ToString(4);
        }
    }
}
