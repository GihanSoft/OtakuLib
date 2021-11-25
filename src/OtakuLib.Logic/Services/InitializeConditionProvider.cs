using System;

using GihanSoft.AppBase;

using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Utilities;

namespace OtakuLib.Logic.Services
{
    public class InitializeConditionProvider : IInitializeConditionProvider
    {
        private readonly MainSettings? mainSettings;
        private readonly Version version;

        public InitializeConditionProvider(ISettingsManager settingsManager, Version version)
        {
            mainSettings = settingsManager.GetMainSettings();
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
