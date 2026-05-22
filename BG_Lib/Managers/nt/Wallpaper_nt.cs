using System;
using System.Runtime.InteropServices;

namespace BG_Lib.Managers.nt
{
    public partial class Wallpaper_nt : IWallpaper
    {
        bool IWallpaper.IsFullySupported => true;
        public IDesktopWallpaper wallpaper;
        private string id = null;

        public Wallpaper_nt()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new PlatformNotSupportedException("Windows Only");

            wallpaper = (IDesktopWallpaper)new DesktopWallpaperComObject();
        }

        public string[] GetMonitors()
        {
            string[] monitors = new string[wallpaper.GetMonitorDevicePathCount()];
            for (uint i = 0; i < monitors.Length; i++) { monitors[i] = wallpaper.GetMonitorDevicePathAt(i); }
            return monitors;
        }
        public void SetMonitor(string id) { this.id = id; }

        public void SetPosition(WallpaperPosition position) { wallpaper.SetPosition(position); }

        public bool SetWallpaper(string filepath)
        {

            try
            {
                Shared_Functions.Setup_Cache(filepath);
                try
                { wallpaper.SetWallpaper(id, Shared_Functions.CACHE_PATH); }
                catch (COMException)
                { Lagacy_Wallpaper.Set(Shared_Functions.CACHE_PATH); }
            }
            catch { return false; }
            return true;
        }
    }
}
