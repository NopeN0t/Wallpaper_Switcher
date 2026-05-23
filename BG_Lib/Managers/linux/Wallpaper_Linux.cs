using System;

namespace BG_Lib.Managers.linux
{
    public partial class Wallpaper_Linux : IWallpaper
    {
        bool IWallpaper.IsFullySupported => false;
        readonly string ENVIROMENT = Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP");

        public string[] GetMonitors()
        {
            //Not supported fornow
            return Array.Empty<string>();
        }

        public void SetMonitor(string id)
        {
            //Not supported fornow
        }

        public void SetPosition(WallpaperPosition position)
        {
            //Not supported fornow
        }

        public bool SetWallpaper(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                return false;
            try
            {
                if (Shared_Functions.Is_Cache_Requried(filepath))
                    filepath = Shared_Functions.CACHE_PATH;

                if (ENVIROMENT?.Contains("GNOME") == true || ENVIROMENT?.Contains("Cinnamon") == true)
                    return Try_Set_GNOME(filepath);

                else if (ENVIROMENT?.Contains("KDE") == true)
                    return Try_Set_KDE(filepath);

                else
                    return false;
            }
            catch { return false; }
        }
    }
}
