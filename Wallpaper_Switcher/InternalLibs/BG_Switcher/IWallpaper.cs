namespace Wallpaper_Switcher.InternalLibs.BG_Switcher
{
    public interface IWallpaper
    {
        bool SetWallpaper(string filepath);
        void SetMonitor(string id);
        void SetPosition(int x, int y);

        string[] GetMonitors();
    }
}
