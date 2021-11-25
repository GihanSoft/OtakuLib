using System.Windows;

using GihanSoft.AppBase;

using OtakuLib.Logic.Services;
using OtakuLib.WPF.Models.Settings;

namespace OtakuLib.WPF.Bootstrap
{
    public class Initializer : IInitializer
    {
        private readonly ISettingsManager settingsManager;

        public Initializer(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
        }

        public void FirstRunInitialize()
        {
            const double width = 600;
            const double height = 450;
            settingsManager.Save(WindowSettings.Key, new WindowSettings
            {
                WindowState = WindowState.Maximized,
                Width = width,
                Height = height,
                Top = (SystemParameters.MaximizedPrimaryScreenHeight - height) / 2,
                Left = (SystemParameters.MaximizedPrimaryScreenWidth - width) / 2,
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
