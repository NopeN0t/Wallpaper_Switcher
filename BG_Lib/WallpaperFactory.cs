using System.Runtime.InteropServices;

namespace BG_Lib
{
    public static class WallpaperFactory
    {
        public static Managers.IWallpaper Create()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new Managers.nt.Wallpaper_nt();

            return new Managers.linux.Wallpaper_Linux();
        }
    }
}
