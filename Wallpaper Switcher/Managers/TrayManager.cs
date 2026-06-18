using Avalonia.Controls;

namespace Wallpaper_Switcher.Managers
{
    public static class TrayManager
    {
        public static TrayIcon? Tray { get; set; }
        public static NativeMenuItem? IndexItem { get; set; }
        public static NativeMenuItem? TimerItem { get; set; }
        public static NativeMenuItem? StartStopItem { get; set; }

        /// <summary>
        /// Updates UI text to given values according to this <code>$"Image {current}/{total}"</code>
        /// </summary>
        /// <param name="current">Current Value</param>
        /// <param name="total">Max Value</param>
        public static void UpdateIndexItem(int current, int total)
        {
            if (IndexItem == null) { return; }
            IndexItem.Header = $"Image {current}/{total}";
        }

        /// <summary>
        /// Updates UI text to given time
        /// </summary>
        /// <param name="time">Time in secounds</param>
        public static void UpdateTimerItem(int time)
        {
            if (TimerItem == null) { return; }
            TimerItem.Header = $"Next in {Globals.SecondsToString(time)}";
        }

        /// <summary>
        /// Update start and stop timer text
        /// </summary>
        public static void UpdateStartStop()
        {
            if (StartStopItem == null) { return; }
            StartStopItem.Header = Globals.bg_switcher.IsRunning ? "Stop Timer" : "Start Timer";
        }
    }
}
