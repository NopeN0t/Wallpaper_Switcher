using Avalonia.Controls;

namespace Wallpaper_Switcher
{
    public partial class MainWindow : Window
    {
        private bool _advancedOpen = false;
        //public bool IsPreviewEmpty = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browse_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
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