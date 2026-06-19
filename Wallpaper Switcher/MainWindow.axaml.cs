using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ImageMagick;
using MsBox.Avalonia;
using System;
using System.IO;
using Wallpaper_Switcher.Managers;

namespace Wallpaper_Switcher
{
    public partial class MainWindow : Window
    {
        private bool _advancedOpen = false;
        public MainWindow()
        {
            InitializeComponent();
            Setup_Lib(ref Globals.bg_switcher);
            this.Closing += (_, _) => { StopTimer(true, true); };
            Globals.PlayAnimation(TrayManager.Tray, this);
        }

        private void Setup_Lib(ref BG_Lib.BG_Switcher lib)
        {
            if (lib.LoadState())
            {
                PathBox.Text = lib.BG_Source;
            }
            lib.TimerTick += (_, _) => { HandleTick(); };
            lib.OnBackgroundChanged += (s, e) => { Globals.PlayAnimation(TrayManager.Tray, this); };

            //Texts
            TimerLabel.Text = $"T: {Globals.SecondsToString(lib.Change_Interval)}";
            ElapsedLabel.Text = $"E: {Globals.SecondsToString(lib.Elapsed)}";
            TimerBox.Text = lib.Change_Interval.ToString();
            AutoSaveBox.Text = lib.AutoSaveInterval.ToString();

            //Functions
            RefreshImages();

            //Tray
            TrayManager.UpdateIndexItem(lib.Image_Index + 1, lib.GetImages().Count);
            TrayManager.UpdateTimerItem(lib.Change_Interval - lib.Elapsed);
        }

        private async void Browse_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //FolderPicker
            var toplevel = TopLevel.GetTopLevel(this);
            if (toplevel is null) return;

            var storage = toplevel.StorageProvider;

            var folder = await storage.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select folder",
                AllowMultiple = false
            });

            //Update values
            if (folder.Count > 0)
            {
                Globals.bg_switcher.BG_Source = folder[0].Path.AbsolutePath;
                PathBox.Text = folder[0].Path.AbsolutePath;
                RefreshImages();
            }
        }

        private void ImageList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            string path = Globals.bg_switcher.GetImages()[ImageList.SelectedIndex];
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var img = new MagickImage(path))
                    {
                        img.Strip();
                        img.Format = MagickFormat.Png;
                        img.Resize(700, 0);
                        img.Write(ms);
                    }
                    ms.Position = 0;
                    { PreviewBox.Source = new Bitmap(ms); }
                }
            }
            catch (Exception ex)
            {
                _ = MessageBoxManager.GetMessageBoxStandard("Wallpaper Switcher", $"Something went wrong when loading preview\n{ex}",
                MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            }
            SelectedImageLabel.Text = $"Slected Image : {ImageList.SelectedIndex + 1}";
        }

        private void ShuffleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }

        private void UnshuffleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }

        private void StartStopButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (Globals.bg_switcher.IsRunning) StopTimer();
            else StartTimer();
        }

        private void ResetTimerButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void AdvancedButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _advancedOpen = !_advancedOpen;

            if (_advancedOpen)
            {
                AdvancedPanel.IsVisible = true;

                AdvancedPanel.Width = 220;
                this.Width += 236;
                this.MinWidth = 636;
            }
            else
            {
                AdvancedPanel.Width = 0;
                this.MinWidth = 400;
                this.Width -= 236;
                AdvancedPanel.IsVisible = false;
            }
        }
    }
}