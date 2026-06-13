using Avalonia.Controls;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;

namespace Wallpaper_Switcher
{
    public static class Globals
    {
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
    }
}
