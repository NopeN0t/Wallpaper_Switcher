using BG_Lib.Managers;
using System;
using System.Collections.Generic;
using System.IO;

using System.Runtime.Serialization.Json;

namespace BG_Lib
{
    public partial class BG_Switcher : IDisposable
    {
        public readonly string CONFIGPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "state.json");
        //Seperated Format list from Imagemagick
        public readonly HashSet<string> SupportedFormats = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { ".jpg", ".jpeg", ".jxl", ".png", ".bmp", ".gif", ".avif", ".heic", "webp" };

        public string BG_Source { get; set; }
        public int Change_Interval { get; set; } = 3600; //60 minutes in seconds
        public int Elapsed { get; set; } = 0; //Seconds
        public int Image_Index { get; set; } = 0;
        public int AutoSaveInterval { get; set; } = 300; //Backup every 5 minutes

        public bool IsRunning { get; private set; } = false;
        public event EventHandler<string> OnBackgroundChanged;
        public event EventHandler<string> TimerTick;

        private readonly List<string> ImageList = new List<string>();
        private System.Timers.Timer timer;
        private readonly object timerLock = new object();

        public IWallpaper Wallpaper { get; } = WallpaperFactory.Create();
        private Shuffler Shuffle { get; } = new Shuffler();

        public void Dispose()
        {
            Stop();
            ImageList.Clear();
        }

        /// <summary>
        /// Shuffle Images Physically
        /// </summary>
        /// <param name="Undo">Un-shuffle images</param>
        public void ShuffleImage(bool Undo = false)
        {
            if (Undo)
                Shuffle.Reset();
            else
                Shuffle.Shuffle(BG_Source);
            LocateImages(true); //Relocate Images
        }

        /// <summary>
        /// Start main Timer
        /// </summary>
        /// <param name="LoadLastSession">Load last session params if available</param>
        /// <exception cref="FileNotFoundException"></exception>
        public void Start(bool LoadLastSession = true)
        {
            if (IsRunning) return;
            if (LoadLastSession) LoadState(); //Load last instance state
            if (LocateImages() == 0) throw new FileNotFoundException("No supported images found");

            timer = new System.Timers.Timer(1000) { AutoReset = false }; // Create Timer
            timer.Elapsed += Timer_Elapsed;
            IsRunning = true; //Initial startup
            timer.Start();
        }
        
        /// <summary>
        /// Handle main timer logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (timerLock)
            {
                Elapsed++;
                TimerTick?.Invoke(this, Elapsed.ToString());
                if (Elapsed % AutoSaveInterval == 0) Save_State(); //Auto save

                if (Elapsed >= Change_Interval)
                {
                    if (Image_Index >= ImageList.Count - 1 || Image_Index < 0) //Loop around
                        Image_Index = 0;

                    Change_BG(Image_Index);
                    Image_Index++; //No C Programming moment here for readablity
                    Elapsed = 0; // Reset state
                }
            }
            if (IsRunning) timer.Start(); // Restart timer
        }
        
        /// <summary>
        /// Stop main timer
        /// </summary>
        public void Stop()
        {
            timer?.Stop();
            IsRunning = false;
        }
        
        /// <summary>
        /// Set Background Image to given index in the list
        /// <para>Prevents overflow/underflow by setting index to 0</para>
        /// <para>Internal Index automatically loops by itself</para>
        /// </summary>
        /// <param name="index"></param>
        public void Change_BG(int index)
        {
            //This prevent overflow/underflow when called externally
            if (index > ImageList.Count - 1 || index < 0) index = 0;
            OnBackgroundChanged?.Invoke(this, ImageList[index]);
            Wallpaper.SetWallpaper(ImageList[index]);
        }
        
        /// <summary>
        /// Saves current state to disk
        /// </summary>
        public void Save_State()
        {
            var state = new SwitcherState()
            {
                BG_Source = BG_Source,
                Change_Interval = Change_Interval,
                Elapsed = Elapsed,
                Image_Index = Image_Index,
                AutoSave_Interval = AutoSaveInterval
            };
            using (var stream = new FileStream(CONFIGPATH, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof(SwitcherState));
                serializer.WriteObject(stream, state);
            }
        }
        
        /// <summary>
        /// Loads state from disk
        /// </summary>
        /// <returns><see langword="true"/> if one Exists and valid otherwise <see langword="false"/></returns>
        public bool LoadState()
        {
            if (!File.Exists(CONFIGPATH)) return false;
            try
            {
                using (var stream = new FileStream(CONFIGPATH, FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(SwitcherState));
                    var state = (SwitcherState)serializer.ReadObject(stream);

                    if (BG_Source == null) BG_Source = state.BG_Source;
                    if (Change_Interval > 0 || state.Change_Interval > 0) Change_Interval = state.Change_Interval;
                    if (AutoSaveInterval > 0 || state.AutoSave_Interval > 0) AutoSaveInterval = state.AutoSave_Interval;
                    Elapsed = state.Elapsed;
                    Image_Index = state.Image_Index;
                }
                if (!Directory.Exists(BG_Source)) return false;
                return true;
            }
            catch
            {
                //To do Add logging system
                return false;
            }
        }
        
        /// <param name="AutoLocate">Locate Image if no images in list</param>
        /// <param name="ForceLocate">Clear and Relocate Images</param>
        /// <returns><see langword="List"/> of Image</returns>
        public List<string> GetImages(bool AutoLocate = true, bool ForceLocate = false)
        {
            //This returns Image paths
            if (ImageList.Count == 0 && AutoLocate) LocateImages();
            if (ForceLocate) { LocateImages(true); }
            return ImageList;
        }
        
        /// <summary>
        /// Recursively searches for Supported Images within BG_Source
        /// <para>Do noting if already search once</para>
        /// </summary>
        /// <param name="ForceLocate">Force locate all images again</param>
        /// <returns>Total Image</returns>
        private int LocateImages(bool ForceLocate = false)
        {
            if (ImageList.Count != 0 && !ForceLocate) return ImageList.Count; // Prevent searching if it already is
            ImageList.Clear(); //Always Clear images List on every search

            foreach (string file in Directory.GetFiles(BG_Source, "*.*", SearchOption.TopDirectoryOnly))
            {
                if (SupportedFormats.Contains(Path.GetExtension(file)))
                    ImageList.Add(file);
            }
            return ImageList.Count; // Total Image
        }
    }
}