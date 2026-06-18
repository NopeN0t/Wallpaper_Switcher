using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Wallpaper_Switcher.Managers;

namespace Wallpaper_Switcher
{
    public partial class MainWindow : Window
    {
        private bool _advancedOpen = false;
        //public bool IsPreviewEmpty = false;
        public MainWindow()
        {
            InitializeComponent();
            Setup_Lib(ref Globals.bg_switcher);
            Globals.PlayAnimation(TrayManager.Tray, this);
        }

        private void Setup_Lib(ref BG_Lib.BG_Switcher lib)
        {
            this.Closing += (_, _) => { StopTimer(true, true); };
            if (lib.LoadState())
            {
                PathBox.Text = lib.BG_Source;
            }

            TimerLabel.Text = $"T: {Globals.SecondsToString(lib.Change_Interval)}";
            TimerBox.Text = lib.Change_Interval.ToString();
            AutoSaveBox.Text = lib.AutoSaveInterval.ToString();
            lib.OnBackgroundChanged += (s, e) => { Globals.PlayAnimation(TrayManager.Tray, this); };
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
            }
        }

        private void ImageList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
        }

        private void ShuffleButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }

        private void ResetButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
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