using MsBox.Avalonia;
using System;
using Wallpaper_Switcher.Managers;

namespace Wallpaper_Switcher
{
    partial class MainWindow
    {
        private bool StopTimer(bool Save = false, bool exit = false)
        {
            try
            {
                Globals.bg_switcher.Stop();
                if (Save && !string.IsNullOrWhiteSpace(PathBox.Text))
                    Globals.bg_switcher.Save_State();
                if (exit) return true;

                StartStopButton.Content = "Start";
                TrayManager.UpdateStartStop();

                ElapsedLabel.Text = "E:" + Globals.SecondsToString(Globals.bg_switcher.Elapsed);
                TrayManager.UpdateTimerItem(Globals.bg_switcher.Elapsed);
                return true;
            }
            catch (Exception ex)
            {
                var msg = MessageBoxManager.GetMessageBoxStandard("Wallpaper Switcher", $"Failed To Stop Timer\n{ex.Message}",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                return false;
            }
        }
    }
}
