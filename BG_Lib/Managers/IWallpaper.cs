namespace BG_Lib.Managers
{
    public interface IWallpaper
    {
        bool SetWallpaper(string filepath);

        void SetMonitor(string id);
        string[] GetMonitors();

        void SetPosition(WallpaperPosition position);

        bool IsFullySupported { get; }

    }
    public enum WallpaperPosition
    {
        Center = 0,
        Tile = 1,
        Stretch = 2,
        Fit = 3,
        Fill = 4,
        Span = 5
    }
}
