using Avalonia.Threading;
using MsBox.Avalonia;
using System;
using System.IO;
using Wallpaper_Switcher.Managers;

namespace Wallpaper_Switcher
{
    partial class MainWindow
    {
        private void HandleTick()
        {
            Dispatcher.UIThread.Post(() =>
            {
                if ((!IsVisible && TrayManager.Tray != null && TrayManager.Tray.IsVisible) == true) return;
                try //This part tends to throw exception when form is closed while updating UI **Old UI Comment**
                {
                    if (IsVisible)
                        ElapsedLabel.Text = "E: " + Globals.SecondsToString(Globals.bg_switcher.Elapsed);
                    if (TrayManager.Tray != null && TrayManager.Tray.IsVisible)
                        TrayManager.UpdateTimerItem(Globals.bg_switcher.Change_Interval - Globals.bg_switcher.Elapsed);
                }
                catch { }
            });
        }
        private bool StartTimer()
        {
            try
            {
                Globals.bg_switcher.Start();
                StartStopButton.Content = "Stop";
                TrayManager.UpdateStartStop();
                return true;
            }
            catch (Exception ex)
            {
                _ = MessageBoxManager.GetMessageBoxStandard("Wallpaper Switcher", $"Failed To Start Timer\n{ex.Message}",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                return false;
            }
        }
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

                ElapsedLabel.Text = "E: " + Globals.SecondsToString(Globals.bg_switcher.Elapsed);
                TrayManager.UpdateTimerItem(Globals.bg_switcher.Change_Interval - Globals.bg_switcher.Elapsed);
                return true;
            }
            catch (Exception ex)
            {
                _ = MessageBoxManager.GetMessageBoxStandard("Wallpaper Switcher", $"Failed To Stop Timer\n{ex.Message}",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                return false;
            }
        }
        private void ResetTimer()
        {
            Globals.bg_switcher.Elapsed = 0;
            ElapsedLabel.Text = "E: " + Globals.SecondsToString(Globals.bg_switcher.Elapsed);
            TrayManager.UpdateTimerItem(Globals.bg_switcher.Change_Interval - Globals.bg_switcher.Elapsed);
        }
        private void RefreshImages()
        {
            ImageList.Items.Clear();
            foreach (var file in Globals.bg_switcher.GetImages(false, true))
                ImageList.Items.Add(Path.GetFileName(file));
            if (Globals.bg_switcher.Image_Index != 0)
                ImageList.SelectedIndex = Globals.bg_switcher.Image_Index;
            else
                ImageList.SelectedIndex = 0;
            ProgressLabel.Text = $"P: {Globals.bg_switcher.Image_Index + 1}/{Globals.bg_switcher.GetImages(false).Count}";
            TrayManager.UpdateIndexItem(Globals.bg_switcher.Image_Index + 1, Globals.bg_switcher.GetImages(false).Count);
        }
        private bool SetImage(int index)
        {
            if (Globals.bg_switcher.GetImages(false).Count == 0) return false;
            if (index < 0) index = Globals.bg_switcher.GetImages().Count - 1;
            else if (index > Globals.bg_switcher.GetImages().Count - 1) index = 0;
            Globals.bg_switcher.Image_Index = index;
            Globals.bg_switcher.Change_BG(index);
            return true;
        }
    }
}
