using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
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
        
        public readonly string CONFIGPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "state.json");
        public string BG_Source { get; set; }
        public int Change_Interval { get; set; } = 1800; //Seconds
        public int Elasped { get; set; } = 0; //Seconds
        public int Image_Index { get; set; } = 0;
        public int AutoSave_Interval { get; set; } = 300; //Backup every 5 min
        public bool IsRunning { get; private set; } = false;
        public event EventHandler<string> OnBackgroundChanged;
        public event EventHandler<string> TimerTick;


        private readonly List<string> Image_List = new List<string>();
        private System.Timers.Timer timer;
        private readonly object timerLock = new object();
        public IWallpaper Wallpaper { get; set; }

        public BG_Switcher()
        {
            //OS detection
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            { Wallpaper = new Wallpaper_nt(); }
        }

        public void Dispose()
        {
            Stop();
            Image_List.Clear();
        }
        public void Start(bool SkipLoad = false)
        {
            if (IsRunning) return;
            if (!SkipLoad)
                Load_State(); //Load last instance state
            if (LocateImages() == 0) throw new Exception("No supported images found");

            timer = new System.Timers.Timer(1000) { AutoReset = false }; // Create Timer
            timer.Elapsed += (s, e) =>
            {
                lock (timerLock)
                {
                    Elasped++;
                    TimerTick?.Invoke(this, Elasped.ToString());
                    if (Elasped % AutoSave_Interval == 0)
                        Save_State(); //Auto save
                    if (Elasped >= Change_Interval)
                    {
                        if (Image_Index >= Image_List.Count - 1) //Loop around ??
                            Image_Index = -1; //Counteract ++ from next line
                        Change_BG(++Image_Index); //Change Image
                        Elasped = 0; // Reset state
                    }
                }
                if (IsRunning) timer.Start(); // Start timer
            };
            IsRunning = true; //Initial startup
            timer.Start();
        }
        public void Stop()
        {
            timer?.Stop();
            IsRunning = false;
        }
        public void Change_BG(int index)
        {
            //This prevent overflow/underflow works sometimes
            if (index > Image_List.Count - 1 || index < 0) index = 0;
            OnBackgroundChanged?.Invoke(this, Image_List[index]);
            Wallpaper.SetWallpaper(Image_List[index]);
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

        public List<string> GetImages(bool AutoLocate = true, bool ForceLocate = false)
        {
            //This returns Image paths
            if (Image_List.Count == 0 && AutoLocate) LocateImages();
            if (ForceLocate) { Image_List.Clear(); LocateImages(true); }
            return Image_List;
        }
        private int LocateImages(bool ForceLocate = false)
        {
            if (Image_List.Count != 0 && !ForceLocate) return Image_List.Count; // Prevent searching if it already is
            //Locate Images Hard to read Edition
            var Extensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            foreach (string file in Directory.GetFiles(BG_Source, "*.*", SearchOption.AllDirectories))
            {
                if (Extensions.Contains(Path.GetExtension(file)))
                    Image_List.Add(file);
            }
            return Image_List.Count; // Total Image
        }
    }
}