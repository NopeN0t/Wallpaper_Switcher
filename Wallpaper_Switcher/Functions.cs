using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallpaper_Switcher
{
    partial class MainForm
    {
        //This class stores custom functions OOP style
        private void PlaySwitchAnimation()
        {
            Task.Run(() =>
            {
                foreach (var frame in frames)
                {
                    NotifyIcon.Icon = frame;
                    this.Invoke(new Action(() => { this.Icon = frame; }));
                    Thread.Sleep(100);
                }
                NotifyIcon.Icon = frames[0];
            });
        }
        private string SecondsToString(int time)
        {
            int sec = time % 60;
            int min = (time / 60) % 60;
            int hrs = (time / 3600) % 24;
            int day = (time / 3600) / 24;
            return $"{day}:{hrs:00}:{min:00}:{sec:00}";
        }
        private void HandleTick()
        {
            if (!this.Visible && !IconMenu.Visible) return;
            try //This part tends to throw exception when form is closed while updating UI
            {
                if (this.Visible)
                {
                    this.Invoke(new Action(() =>
                    {
                        Elapsed.Text = "Elapsed =  " + SecondsToString(bg_switcher.Elasped);
                    }));
                }
                if (IconMenu.Visible)
                {
                    this.Invoke(new Action(() =>
                    {

                    }));
                }
            }
            catch { }
        }
        private void StartTimer()
        {
            try { bg_switcher.Start(); }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Hide();
        }
        private void RefreshImages()
        {
            DemoList.Items.Clear();
            foreach (var file in bg_switcher.GetImages())
                DemoList.Items.Add(Path.GetFileName(file));
            if (bg_switcher.Image_Index != 0)
                DemoList.SelectedIndex = bg_switcher.Image_Index;
            else
                DemoList.SelectedIndex = 0;
            Total_Text.Text = "Total Image : " + bg_switcher.GetImages().Count.ToString();
        }
    }
}
