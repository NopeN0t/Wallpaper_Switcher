using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System.Runtime.CompilerServices;

namespace Wallpaper_Switcher
{
    public partial class App : Application
    {
        private TrayIcon? _TrayIcon;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                SetupTray(desktop);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void SetupTray(IClassicDesktopStyleApplicationLifetime desktop)
        {
            _TrayIcon = new TrayIcon
            {
                ToolTipText = "Wallpaper Switcher",
                Icon = Globals.Icons_Animation[0],
                Menu = []
            };

            _TrayIcon.Clicked += (s, e) =>
            {
                var win = desktop.MainWindow;
                if (win == null) return;
                //await Globals.PlaySwitchAnimation(icon =>
                //{
                //    _TrayIcon.Icon = icon;
                //    if (desktop.MainWindow != null) desktop.MainWindow.Icon = icon;
                //});

                win.Show();
                win.WindowState = WindowState.Normal;
                win.Activate();
            };
        }
    }
}