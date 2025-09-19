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
            try //This part explodes when program failed to load config
            {
                if (bg_switcher.Image_Index > bg_switcher.GetImages().Count - 1 || bg_switcher.Image_Index < 0) bg_switcher.Image_Index = 0;
                this.Invoke(new Action(() =>
                {
                    DemoList.SelectedIndex = bg_switcher.Image_Index;
                    Index_Strip.Text = $"Image {bg_switcher.Image_Index + 1}/{bg_switcher.GetImages().Count}";
                }));
                {
                    //Actual Animation
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
                bg_switcher.Elasped = 0; //(Should) Prevents sutting down at the same time as it changes
                bg_switcher.Save_State();
            }
            catch { }
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
                        NextTimer_Strip.Text = $"Next in {SecondsToString(bg_switcher.Change_Interval - bg_switcher.Elasped)}";
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
                MessageBox.Show("Failed To Start Timer\n" + ex.Message, "Wallpaper Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
