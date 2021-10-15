using System;

using OtakuLib.View;

namespace OtakuLib.Host.Bootstrap
{
    public static class Program
    {
        [STAThread]
        public static int Main()
        {
            App app = new();
            app.InitializeComponent();

            Win win = new();

            return app.Run(win);
        }
    }
}
