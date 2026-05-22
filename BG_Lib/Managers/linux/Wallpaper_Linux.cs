using System;

namespace BG_Lib.Managers.linux
{
    internal class Wallpaper_Linux : IWallpaper
    {
        bool IWallpaper.IsFullySupported => false;
        public string[] GetMonitors()
        {
            throw new NotImplementedException();
        }

        public void SetMonitor(string id)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(WallpaperPosition position)
        {
            throw new NotImplementedException();
        }

        public bool SetWallpaper(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                return false;
            return false;
        }
    }
}
