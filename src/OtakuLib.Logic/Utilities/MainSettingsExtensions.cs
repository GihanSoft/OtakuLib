using System;

using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Services;

namespace OtakuLib.Logic.Utilities
{
    internal static class MainSettingsExtensions
    {
        public static MainSettings GetMainSettings(this ISettingsManager optionManager)
        {
            if (optionManager is null)
            {
                throw new ArgumentNullException(nameof(optionManager));
            }

            return optionManager.Fetch<MainSettings>(MainSettings.Key);
        }

        public static void SaveMainSettings(this ISettingsManager optionManager, MainSettings mainSettings)
        {
            if (optionManager is null)
            {
                throw new ArgumentNullException(nameof(optionManager));
            }

            if (mainSettings is null)
            {
                throw new ArgumentNullException(nameof(mainSettings));
            }

            optionManager.Save(MainSettings.Key, mainSettings);
        }
    }
}
