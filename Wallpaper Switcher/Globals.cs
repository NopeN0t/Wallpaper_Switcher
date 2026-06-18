using Avalonia.Controls;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;

namespace Wallpaper_Switcher
{
    public static class Globals
    {
        public static BG_Lib.BG_Switcher bg_switcher = new();
        public static readonly WindowIcon[] Icons_Animation =
        [
            LoadIcon("avares://WallpaperSwitcher/Assets/Icon_0.ico"),
            LoadIcon("avares://WallpaperSwitcher/Assets/Icon_1.ico"),
            LoadIcon("avares://WallpaperSwitcher/Assets/Icon_2.ico"),
            LoadIcon("avares://WallpaperSwitcher/Assets/Icon_3.ico"),
            LoadIcon("avares://WallpaperSwitcher/Assets/Icon_4.ico"),
            LoadIcon("avares://WallpaperSwitcher/Assets/Icon_5.ico"),
        ];

        /// <summary>
        /// Loads Icon from avalonia ui reference path
        /// </summary>
        /// <param name="path">avares:[path]</param>
        /// <returns>Loaded Icon</returns>
        public static WindowIcon LoadIcon(string path)
        {
            var uri = new Uri(path);
            return new WindowIcon(AssetLoader.Open(uri));
        }

        /// <summary>
        /// Play Icon Animation based on Icons_Animation in Global.cs
        /// </summary>
        /// <param name="applyFrame"> Lambda for object to apply to</param>
        /// <param name="delay">delay between each frames in ms</param>
        /// <returns></returns>
        public static async Task PlaySwitchAnimation(
            Action<WindowIcon> applyFrame,
            int delay = 100)
        {
            foreach (var frame in Icons_Animation)
            {
                applyFrame(frame);
                await Task.Delay(delay);
            }
            applyFrame(Icons_Animation[0]); //Assumes Last frame is not identical to first
        }

        /// <summary>
        /// Convert numbers to time string
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string SecondsToString(int time)
        {
            int sec = time % 60;
            int min = (time / 60) % 60;
            int hrs = (time / 3600) % 24;
            int day = (time / 3600) / 24;
            return $"{day}:{hrs:00}:{min:00}:{sec:00}";
        }

        /// <summary>
        /// Set background image to given index loops automatically if overflow
        /// </summary>
        /// <param name="index">image index</param>
        /// <returns><see langword="true"/> if success otherwise <see langword="false"/></returns>
        public static bool SetImage(int index)
        {
            if (bg_switcher.GetImages(false).Count == 0) return false;
            if (index < 0) index = bg_switcher.GetImages().Count - 1;
            else if (index > bg_switcher.GetImages().Count - 1) index = 0;
            bg_switcher.Image_Index = index;
            bg_switcher.Change_BG(index);
            return true;
        }
        
        /// <summary>
        /// Play animation on given desktop or window
        /// </summary>
        /// <param name="tray">System Tray</param>
        /// <param name="win">Main Window</param>
        public async static void PlayAnimation(TrayIcon? tray = null, Window? win = null)
        {
            await PlaySwitchAnimation(icon =>
            {
                if (tray != null) { tray.Icon = icon; }
                if (win != null) { win.Icon = icon; }
            });
        }
    }
}
