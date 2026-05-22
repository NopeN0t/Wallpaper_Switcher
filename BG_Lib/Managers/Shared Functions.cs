using ImageMagick;
using System;
using System.IO;

namespace BG_Lib.Managers
{
    public static class Shared_Functions
    {
        public static string PROGRAM_PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static string CACHE_PATH = Path.Combine(PROGRAM_PATH, "Cache.png");
        
        /// <summary>
        /// Check if givenfile required Caching true if it does false if isn't
        /// </summary>
        /// <param name="filepath">Input file</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static bool Is_Cache_Requried(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File Not Found {filepath}");

            //Formats that known to works across all platforms            
            string ext = Path.GetExtension(filepath).ToLower();
            if (ext == ".png" || ext == ".jpg" || ext == "jpeg")
                return false; 

            //Use Image magick here so things don't get complicated
            using (var img = new MagickImage(filepath))
            {
                img.Strip();
                img.Format = MagickFormat.Png;
                img.Write(CACHE_PATH);
                return true;
            }
        }
    }
}
