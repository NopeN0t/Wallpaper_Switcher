using System;
using System.Runtime.InteropServices;

namespace BG_Lib.Managers.nt
{
    public partial class Wallpaper_nt
    {
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

        [ComImport]
        [Guid("C2CF3110-460E-4FC1-B9D0-8A1C0C9CC4BD")]
        private class DesktopWallpaperComObject { }

        [ComImport]
        [Guid("B92B56A9-8B55-4E14-9A89-0199BBB6F93B")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDesktopWallpaper
        {
            void SetWallpaper([MarshalAs(UnmanagedType.LPWStr)] string monitorID,
                              [MarshalAs(UnmanagedType.LPWStr)] string wallpaper);

            [return: MarshalAs(UnmanagedType.LPWStr)]
            string GetWallpaper([MarshalAs(UnmanagedType.LPWStr)] string monitorID);

            [return: MarshalAs(UnmanagedType.LPWStr)]
            string GetMonitorDevicePathAt(uint monitorIndex);

            uint GetMonitorDevicePathCount();

            void SetBackgroundColor(uint color);
            uint GetBackgroundColor();

            void SetPosition(WallpaperPosition position);
            WallpaperPosition GetPosition();

            void SetSlideshow(IntPtr items);
            IntPtr GetSlideshow();

            void SetSlideshowOptions(uint options, uint slideshowTick);
            void AdvanceSlideshow([MarshalAs(UnmanagedType.LPWStr)] string monitorID, uint direction);
        }
    }
}
