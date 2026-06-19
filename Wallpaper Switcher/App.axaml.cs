using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Threading;
using Wallpaper_Switcher.Managers;

namespace Wallpaper_Switcher
{
    public partial class App : Application
    {
        private TrayIcon? _TrayIcon;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            //Singleton mutex
            //using Mutex mutex = new(true, "Wallpaper_Switcher", out bool isNewInstance);
            //if (!isNewInstance)
            //{
            //    var box = MessageBoxManager.GetMessageBoxStandard
            //                ("Instance Running", "Another instance of the program is already running.",
            //                ButtonEnum.Ok, Icon.Info);
            //    await box.ShowAsync();
            //    Environment.Exit(0);
            //}

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                SetupTray(desktop);
                desktop.MainWindow = new MainWindow();
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
                
                win.Show();
                win.WindowState = WindowState.Normal;
                win.Activate();
            };

            //Information Items
            var IndexItem = new NativeMenuItem("Image 0000/0000")
            {
                IsEnabled = false
            };

            var NextTimerItem = new NativeMenuItem("Next in 0:00:00:00")
            {
                IsEnabled = false
            };

            //Standard Items
            var StartStopItem = new NativeMenuItem("Start Timer");
            StartStopItem.Click += (_, _) => { TrayManager.UpdateStartStop(); };
            
            var ExitItem = new NativeMenuItem("Exit");
            ExitItem.Click += (_, _) => { Environment.Exit(0); };

            //Nested Item - Switch Image
            var NextItem = new NativeMenuItem("Next Image");
            NextItem.Click += (_, _) => { Globals.SetImage(Globals.bg_switcher.Image_Index + 1); };

            var BackItem = new NativeMenuItem("Last Image");
            BackItem.Click += (_, _) => { Globals.SetImage(Globals.bg_switcher.Image_Index - 1); };

            var SwitchImageItem = new NativeMenuItem("Switch Image")
            {
                Menu = new NativeMenu() { Items = { NextItem, BackItem } }
            };

            //Apply Items
            _TrayIcon.Menu.Items.Add(IndexItem);
            _TrayIcon.Menu.Items.Add(NextTimerItem);
            _TrayIcon.Menu.Items.Add(SwitchImageItem);
            _TrayIcon.Menu.Items.Add(StartStopItem);
            _TrayIcon.Menu.Items.Add(ExitItem);

            //Upload to cloud
            TrayManager.Tray = _TrayIcon;
            TrayManager.IndexItem = IndexItem;
            TrayManager.TimerItem = NextTimerItem;
            TrayManager.StartStopItem = StartStopItem;
        }
    }
}