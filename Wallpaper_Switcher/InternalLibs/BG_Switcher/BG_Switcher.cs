using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Wallpaper_Switcher.InternalLibs.BG_Switcher
{
    [DataContract]
    class SwitcherState
    {
        [DataMember] public string BG_Source { get; set; }
        [DataMember] public int Change_Interval { get; set; }
        [DataMember] public int Elasped { get; set; }
        [DataMember] public int Image_Index { get; set; }
        [DataMember] public int AutoSave_Interval { get; set; }
    }

    public class BG_Switcher : IDisposable
    {
        public string BG_Source { get; set; }
        public int Change_Interval { get; set; } = 1800; //Seconds
        public int Elasped { get; set; } = 0; //Seconds
        public int Image_Index { get; set; } = 0;
        public int AutoSave_Interval { get; set; } = 300; //Backup every 5 min
        public bool IsRunning { get; private set; } = false;
        public event EventHandler<string> OnBackgroundChanged;
        public event EventHandler<string> TimerTick;


        private readonly List<string> Image_List = new List<string>();
        private readonly string CONFIGPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "state.json");
        private System.Timers.Timer timer;

        public void Dispose()
        {
            Stop();
            Image_List.Clear();
        }
        public void Start()
        {
            if (IsRunning) return;
            Load_State();

            LocateImages();
            if (Image_List.Count == 0) throw new Exception("No supported images found");


            //Main timer logic
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += (s, e) =>
            {
                Elasped++;
                TimerTick?.Invoke(this, Elasped.ToString());
                if (Elasped % AutoSave_Interval == 0) //Auto save
                    Save_State();
                if (Elasped >= Change_Interval) //Timer
                {
                    Change_BG(++Image_Index);
                    Elasped = 0;
                }
            };
            IsRunning = true;
            timer.Start();
        }
        public void Stop()
        {
            timer?.Stop();
        }
        public void Change_BG(int index)
        {
            //This prevent overflow/underflow
            if (index > Image_List.Count - 1 || index < 0) index = 0;
            OnBackgroundChanged?.Invoke(this, Image_List[index]);
            Wallpaper.Set(Image_List[index]);
        }
        public void Save_State()
        {
            var state = new SwitcherState()
            {
                BG_Source = this.BG_Source,
                Change_Interval = this.Change_Interval,
                Elasped = this.Elasped,
                Image_Index = this.Image_Index,
                AutoSave_Interval = this.AutoSave_Interval
            };
            using (var stream = new FileStream(CONFIGPATH, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof(SwitcherState));
                serializer.WriteObject(stream, state);
            }
        }
        public bool Load_State()
        {
            if (!File.Exists(CONFIGPATH)) return false;
            using (var stream = new FileStream(CONFIGPATH, FileMode.Open))
            {
                var serializer = new DataContractJsonSerializer(typeof(SwitcherState));
                var state = (SwitcherState)serializer.ReadObject(stream);
                if (BG_Source == null) BG_Source = state.BG_Source;
                if (Change_Interval != 1800 || state.Change_Interval != 1800) Change_Interval = state.Change_Interval;
                if (AutoSave_Interval != 300 || state.AutoSave_Interval != 300) AutoSave_Interval = state.AutoSave_Interval;
                Elasped = state.Elasped;
                Image_Index = state.Image_Index;
            }
            if (!Directory.Exists(BG_Source))
                return false;
            return true;
        }

        public List<string> GetImages()
        {
            if (Image_List.Count == 0) LocateImages();
            return Image_List;
        }
        private void LocateImages(bool ForceLocate = false)
        {
            if (Image_List.Count != 0 && !ForceLocate) return;
            //Locate Images Hard to read Edition
            var allowedExts = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            foreach (string file in Directory.GetFiles(BG_Source, "*.*", SearchOption.AllDirectories))
            {
                if (allowedExts.Contains(Path.GetExtension(file)))
                    Image_List.Add(file);
            }
        }
    }
    class Wallpaper
    {
        const int SPI_SETDESKWALLPAPER = 20;
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
}