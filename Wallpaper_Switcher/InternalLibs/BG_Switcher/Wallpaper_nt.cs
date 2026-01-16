using System;
using System.Runtime.InteropServices;

namespace Wallpaper_Switcher.InternalLibs.BG_Switcher
{
    public class Wallpaper_nt : IWallpaper
    {
        public IDesktopWallpaper wallpaper;
        private string id = null;
        [ComImport]
        [Guid("C2CF3110-460E-4FC1-B9D0-8A1C0C9CC4BD")]
        private class DesktopWallpaperComObject { }
        private class Lagacy_Wallpaper
        {
            const int SPI_SETDESKWALLPAPER = 20; //0x14
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDCHANGE = 0x02;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern int SystemParametersInfo(
                int uAction, int uParam, string lpvParam, int fuWinIni);

            public static void Set(string filePath)
            {
                // filePath should be BMP, JPG, PNG (Windows will convert internally if needed)
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filePath,
                    SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
            }
        }
       
        public Wallpaper_nt()
        {
            wallpaper = (IDesktopWallpaper)new DesktopWallpaperComObject();
        }
        public void SetMonitor(string id) { this.id = id; }
        public void SetPosition(int x, int y = 0){ wallpaper.SetPosition((DesktopWallpaperPosition)x); }
        public bool SetWallpaper(string filepath)
        {
            try
            {
                try
                {
                    wallpaper.SetWallpaper(id, filepath);
                }
                catch { Lagacy_Wallpaper.Set(filepath); }
            }
            catch { return false; }
            return true;
        }
        public string[] GetMonitors() 
        {
            string[] monitors = new string[wallpaper.GetMonitorDevicePathCount()];
            for (uint i = 0; i < monitors.Length; i++) { monitors[i] = wallpaper.GetMonitorDevicePathAt(i); }
            return monitors;
        }
    }
}
