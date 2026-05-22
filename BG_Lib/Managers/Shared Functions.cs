using ImageMagick;
using System;
using System.IO;

namespace BG_Lib.Managers
{
    public static class Shared_Functions
    {
        public static string PROGRAM_PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static string CACHE_PATH = Path.Combine(PROGRAM_PATH, "Cache.png");
        public static void Setup_Cache(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File Not Found {filepath}");

            //Use Image magick here so things don't get complicated
            using (var img = new MagickImage(filepath))
            {
                img.Strip();
                img.Format = MagickFormat.Png;
                img.Write(CACHE_PATH);
            }
        }
    }
}
