using System;

using GihanSoft.AppBase;

using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Services;
using OtakuLib.Logic.Utilities;

namespace OtakuLib.Logic.Bootstrap
{
    public class Initializer : IInitializer
    {
        private readonly ISettingsManager settingsManager;
        private readonly Version version;

        public Initializer(ISettingsManager settingsManager, Version version)
        {
            this.settingsManager = settingsManager;
            this.version = version;
        }

        public void FirstRunInitialize()
        {
            settingsManager.SaveMainSettings(new MainSettings
            {
                Version = version.ToString(4),
                AppearanceSettings = new AppearanceSettings
                {
                }
            });
        }

        public void Initialize()
        {
        }

        public void UpdateInitialize()
        {
        }
    }
}
